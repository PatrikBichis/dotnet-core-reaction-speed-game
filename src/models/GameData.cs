using System;

namespace dotnet_core_reaction_speed_game.models
{
    public class GameData{
        // Nr of buttons that have been illuminated
        public int Presses {get;set;}

        // Score for the current game session
        public int TotalScore {get;set;}

        public TimeSpan TotalTime;

        public int NrOfWrongPresses {get;set;}

        public GameData()
        {
            this.Presses = 0;
            this.TotalScore = 0;
            this.TotalTime = TimeSpan.FromSeconds(0);
            this.NrOfWrongPresses = 0;
        }
    }
}