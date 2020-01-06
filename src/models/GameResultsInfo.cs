using System;
using System.Collections;
using System.Collections.Generic;
using dotnet_core_reaction_speed_game.enums;

namespace dotnet_core_reaction_speed_game.models
{
    public class GameResultInfo{

        public string ClientId {get;set;}
        public string Topic {get;set;}
        public GameData Result {get;set;}
        public List<SessionInfo> Sessions {get;set;}

        public GameResultInfo(string clientId, GameData result, List<SessionInfo> sessions)
        {
            ClientId = clientId;
            Topic = "reaction-speed-game/" + ClientId + "/result";
            Result = result;
            Sessions = sessions;  
        }
    }
}