using System;

namespace dotnet_core_reaction_speed_game{
    public class KeyboardService : IDeviceService{

        private bool _correctButton = false;
        private LedType[] _leds = new LedType[5]{LedType.LED1,LedType.LED2,LedType.LED3,LedType.LED4,LedType.LED5};
        
        private SwitcheType[] _switches = new SwitcheType[5]{SwitcheType.Switch1,SwitcheType.Switch2,SwitcheType.Switch3,SwitcheType.Switch4,SwitcheType.Switch5};

        private bool[] _ledStatus = new bool[5]{false,false,false,false,false};
        private ConsoleKey[] Keys = new ConsoleKey[5]{ConsoleKey.D1,ConsoleKey.D2,ConsoleKey.D3,ConsoleKey.D4,ConsoleKey.D5};

        public bool CorrectButtonPressed{get{return _correctButton;} set{value = _correctButton;}}

        public LedType[] Leds {get{return _leds;} set{value = _leds;}}
        public SwitcheType[] Switches {get{return _switches;} set {value = _switches;}}
        public bool[] LedStatus {get{return _ledStatus;} set{value = _ledStatus;}}


        public void Init(){

        }

        public bool WaitForButtonPressInGeme(int index){
            return WaitForKeyPressedInGame(index);
        }

        public bool WaitForButtonPress(int index){
            var key = ConsoleKey.NoName;

            if(index == 0) key = ConsoleKey.D1;
            if(index == 1) key = ConsoleKey.D2;
            if(index == 2) key = ConsoleKey.D3;
            if(index == 3) key = ConsoleKey.D4;
            if(index == 4) key = ConsoleKey.D5;

            return WaitForKeyPressed(key);
        }

        private bool WaitForKeyPressedInGame(int keyIndex){
            var lastKey = ConsoleKey.NoName;
            var RunGame = true;
            while(lastKey == ConsoleKey.NoName){
                Console.WriteLine("Press key: ");
                var choice = Console.ReadKey(true).Key;
                switch (choice)
                {
                    // 1 ! key
                    case ConsoleKey.D1:
                        Console.WriteLine("Key 1 pressed");
                        lastKey = ConsoleKey.D1;
                        break;
                    //2 @ key
                    case ConsoleKey.D2:
                        Console.WriteLine("Key 2 pressed");
                        lastKey = ConsoleKey.D2;
                        break;
                    //3 @ key
                    case ConsoleKey.D3:
                        Console.WriteLine("Key 3 pressed");
                        lastKey = ConsoleKey.D3;
                        break;
                    //4 @ key
                    case ConsoleKey.D4:
                        Console.WriteLine("Key 4 pressed");
                        lastKey = ConsoleKey.D4;
                        break;
                    //5 @ key
                    case ConsoleKey.D5:
                        Console.WriteLine("Key 5 pressed");
                        lastKey = ConsoleKey.D5;
                        break;
                    //ESC @ key
                    case ConsoleKey.Escape:
                        Console.WriteLine("Closing game...");
                        RunGame = false;
                        break;
                }

                
            }

            _correctButton = false;
                
            if(lastKey == Keys[keyIndex]) _correctButton = true;

            return RunGame;
        }

        private bool WaitForKeyPressed(ConsoleKey key){
            var lastKey = ConsoleKey.NoName;
            var RunGame = true;

            while (lastKey != key){
                var choice = Console.ReadKey(true).Key;
                switch (choice)
                {
                    // 1 ! key
                    case ConsoleKey.D1:
                        Console.WriteLine("Key 1 pressed");
                        lastKey = ConsoleKey.D1;
                        break;
                    //2 @ key
                    case ConsoleKey.D2:
                        Console.WriteLine("Key 2 pressed");
                        lastKey = ConsoleKey.D2;
                        break;
                    //3 @ key
                    case ConsoleKey.D3:
                        Console.WriteLine("Key 3 pressed");
                        lastKey = ConsoleKey.D3;
                        break;
                    //4 @ key
                    case ConsoleKey.D4:
                        Console.WriteLine("Key 4 pressed");
                        lastKey = ConsoleKey.D4;
                        break;
                    //5 @ key
                    case ConsoleKey.D5:
                        Console.WriteLine("Key 5 pressed");
                        lastKey = ConsoleKey.D5;
                        break;
                    //ESC @ key
                    case ConsoleKey.Escape:
                        Console.WriteLine("Closing game...");
                        RunGame = false;
                        break;
                }
            }

            return RunGame;
        }


        public void SetAllLedStates(bool state){
            foreach (var led in _leds)
            {
                SetLedState(led, state);
            }
        }

        public void SetLedState(LedType led, bool state){
            
            if(led == LedType.LED1)
                _ledStatus[0] = state;
            else if(led == LedType.LED2)
                _ledStatus[1] = state;
            else if(led == LedType.LED3)
                _ledStatus[2] = state;
            else if(led == LedType.LED4)
                _ledStatus[3] = state;
            else if(led == LedType.LED5)
                _ledStatus[4] = state; 
        }
    }
}   