namespace dotnet_core_reaction_speed_game{
    public interface IDeviceService
    {

        LedType[] Leds {get;set;}

        SwitcheType[] Switches {get;set;}

        bool[] LedStatus {get;set;}
        bool CorrectButtonPressed {get;set;}

        void Init();

        void SetLedState(LedType led, bool state);

        void SetAllLedStates(bool state);

        bool WaitForButtonPressInGeme(int index);

        bool WaitForButtonPress(int index);
    }
}