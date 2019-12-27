using System;
using System.Device.Gpio;
using System.Threading;
using dotnet_core_reaction_speed_game.enums;

namespace dotnet_core_reaction_speed_game{
    public class GpioSerivce : IDeviceService
    {
        public LedType[] Leds { get => _leds; set => value = _leds; }
        public SwitcheType[] Switches { get => _switches; set => value = _switches; }
        public bool[] LedStatus { get => _ledStatus; set => value = _ledStatus; }
        public bool CorrectButtonPressed { get{ return _correctButton;} set{value = _correctButton;} }

        private bool _correctButton = false;
        private LedType[] _leds = new LedType[5]{LedType.LED1,LedType.LED2,LedType.LED3,LedType.LED4,LedType.LED5};
        
        private SwitcheType[] _switches = new SwitcheType[5]{SwitcheType.Switch1,SwitcheType.Switch2,SwitcheType.Switch3,SwitcheType.Switch4,SwitcheType.Switch5};

        private bool[] _ledStatus = new bool[5]{false,false,false,false,false};

        private GpioController controller; 

        public GpioSerivce()
        {
            controller = new GpioController();
        }

        public void Init()
        {
            // Init leds
            foreach (var led in Leds)
            {
                controller.OpenPin((int)led, PinMode.Output);
            }

            // Init switches
            foreach (var _switch in Switches)
            {
                controller.OpenPin((int)_switch, PinMode.Input);
                //controller.RegisterCallbackForPinValueChangedEvent((int)_switch, PinEventTypes.Falling, OnSwitchPressed);
            }
        }

        public void Close(){

            // Turn of all leds
            SetAllLedStates(false);

            // Close leds
            foreach (var led in Leds)
            {
                controller.ClosePin((int)led);
            }

            // Close switches
            foreach (var _switch in Switches)
            {
                //controller.UnregisterCallbackForPinValueChangedEvent((int) _switch, OnSwitchPressed);
                controller.ClosePin((int)_switch);
            }

            controller.Dispose();
        }

        public void SetAllLedStates(bool state)
        {
            foreach (var led in _leds)
            {
                SetLedState(led, state);
            }
        }

        public void SetLedState(LedType led, bool state)
        {
             controller.Write((int)led, state ? PinValue.High : PinValue.Low);
        }


        private void OnSwitchPressed(object sender, PinValueChangedEventArgs pinValueChangedEventArgs){
            Console.WriteLine(pinValueChangedEventArgs.PinNumber);
        }

        // Wait for a specific button to be pressed
        public bool WaitForButtonPress(int index)
        {
            var buttonPressed = false;

            try{
                while(!buttonPressed){
                    if(controller.Read((int)Switches[index]) == PinValue.High){
                        buttonPressed = true;
                        Console.WriteLine("Key {0} pressed", index + 1);
                        break;
                    }
                }
            }catch(Exception ex){
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }

        // Wait for any switch to be closed and check if it's the correct one.
        public bool WaitForButtonPressInGame(int index)
        {
            var pressedIndex = -1;
            _correctButton = false;

            try{
                while(pressedIndex == -1){
                    for (int i = 0; i < Switches.Length; i++)
                    {
                        if(controller.Read((int) Switches[i]) == PinValue.High){
                            // Turn of led when the button is pressed
                            SetLedState(Leds[i], false);
                            pressedIndex = i;
                            break;
                        }
                    }
                }

                // Debounce timeout
                Thread.Sleep(150);

                // Check that the button is released
                while(controller.Read((int) Switches[pressedIndex]) != PinValue.Low){
                    Thread.Sleep(150);
                }

                // Debounce timeout
		        Thread.Sleep(150);

                // Check if the correct button was pressed
                if(pressedIndex == (int)Switches[index]) {
                    _correctButton = true;
                    Console.WriteLine("Key {0} pressed", Switches[pressedIndex]);
                } else {
                    if(pressedIndex >= 0 && pressedIndex <= Switches.Length -1){
                        Console.WriteLine("Wrong key pressed!!!({0} -> {1})", Switches[pressedIndex], Switches[index]);
                    }else Console.WriteLine("Not a valid index on the pressed button!");
                }

            }catch(Exception ex){
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }
    }
}
