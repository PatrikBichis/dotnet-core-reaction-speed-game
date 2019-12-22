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

        public void Init()
        {
            throw new System.NotImplementedException();
        }

        public void SetAllLedStates(bool state)
        {
            throw new System.NotImplementedException();
        }

        public void SetLedState(LedType led, bool state)
        {
            throw new System.NotImplementedException();
        }

        public bool WaitForButtonPress(int index)
        {
            throw new System.NotImplementedException();
        }

        public bool WaitForButtonPressInGeme(int index)
        {
            throw new System.NotImplementedException();
        }
    }
}