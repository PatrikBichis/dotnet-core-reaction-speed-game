using System;
using System.Device.Gpio;
using dotnet_core_reaction_speed_game.enums;

namespace dotnet_core_reaction_speed_game{
    public class GpioSerivce : IDeviceService
    {
        public LedType[] Leds { get => _leds; set => value = _leds; }
        public SwitcheType[] Switches { get => _switches; set => value = _switches; }
        public bool[] LedStatus { get => _ledStatus; set => value = _ledStatus; }
        public bool CorrectButtonPressed { get => _correctButton; set{value = _correctButton;} }

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
            }
        }

        public void Close(){
            // Close leds
            foreach (var led in Leds)
            {
                controller.ClosePin((int)led);
            }

            // Close switches
            foreach (var _switch in Switches)
            {
                controller.ClosePin((int)_switch);
            }
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

        // Wait for a specific button to be pressed
        public bool WaitForButtonPress(int index)
        {
            var buttonPressed = false;

            try{
                while(!buttonPressed){
                    if(controller.Read((int)Switches[index]) == PinValue.Low){
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
            var indexPressed = -1;

            try{
                while(indexPressed == -1){
                    foreach (var _switch in Switches)
                    {
                        if(controller.Read((int)Switches[index]) == PinValue.Low){
                            indexPressed = (int) _switch;
                            break;
                        }
                    }
                }

                _correctButton = false;
                
                // Check if the correct button was pressed
                if(indexPressed == (int)Switches[index]) {
                    _correctButton = true;
                    Console.WriteLine("Key {0} pressed", indexPressed + 1);
                } else {
                    Console.WriteLine("Wrong key {0} pressed!!!", indexPressed + 1);
                }

            }catch(Exception ex){
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }
    }
}