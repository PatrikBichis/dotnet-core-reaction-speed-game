using System;
using dotnet_core_reaction_speed_game.enums;

namespace dotnet_core_reaction_speed_game.models
{
    public class GameStateInfo{

        public string ClientId {get;set;}
        public string Topic {get;set;}
        public string State {get;set;}
        public int CountDown {get;set;}

        public GameStateInfo(string clientId, GameState state, int countDown)
        {
            ClientId = clientId;
            Topic = "reaction-speed-game/" + ClientId + "/state/";
            State = state.ToString();
            CountDown = countDown;
        }
    }
}