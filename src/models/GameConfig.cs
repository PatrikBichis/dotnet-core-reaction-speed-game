using System;

namespace dotnet_core_reaction_speed_game.models
{
    public class GameConfig{
        public bool RunGame {get;set;}
        public bool UseGpio {get;set;}

        public bool SendMqttInfo {get;set;}

        // Nr of buttons to press during a game
        public int NrOfLoop {get;set;}

        public int MaxPoints {get;set;}
        public int Deduction {get;set;}

        public GameConfig()
        {
            this.RunGame = true;
            this.UseGpio = false;
            this.SendMqttInfo = false;
            this.NrOfLoop = 10;
            this.MaxPoints = 10;
            this.Deduction = 5;
        }
    }
}