using System;

namespace dotnet_core_reaction_speed_game.models
{
    public class SessionInfo{

        public int Id {get;set;}
        public string ClientId {get;set;}
        public string Topic {get;set;}
        public bool? State {get;set;}
        public int Score {get;set;}
        public int Seconds {get;set;}

        public SessionInfo(string clientId, int id, bool? state, int score, int seconds)
        {
            Id = id;
            ClientId = clientId;

            Topic = "reaction-speed-game/" + ClientId + "/session/"+ id;
            State = state;
            Score = score;
            Seconds = seconds;
        }
    }
}