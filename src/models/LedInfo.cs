using System;

namespace dotnet_core_reaction_speed_game.models
{
    public class LedInfo{

        public int Id {get;set;}
        public string ClientId {get;set;}
        public string Topic {get;set;}
        public bool Payload {get;set;}

        public LedInfo(string clientId, int id, bool state)
        {
            Id = id;
            ClientId = clientId;
            Topic = "reaction-speed-game/" + ClientId + "/led/"+ id;
            Payload = state;
        }
    }
}