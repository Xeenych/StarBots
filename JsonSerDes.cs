using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GameLib
{
    public static class JsonSerDes
    {
        public static string Serialize(GameInfo info)
        {
            string ret = JsonConvert.SerializeObject(info);
            return ret;
        }

        public static string Serialize(GameState info)
        {
            string ret = JsonConvert.SerializeObject(info);
            return ret;
        }

        public static string Serialize(PlayerAction action)
        {
            string ret = JsonConvert.SerializeObject(action);
            return ret;
        }

        public static GameState DeserializeGameState(string s)
        {
            return JsonConvert.DeserializeObject<GameState>(s);
        }

        public static GameInfo DeserializeGameInfo(string s)
        {
            return JsonConvert.DeserializeObject<GameInfo>(s);
        }

        public static PlayerAction DeserializeAction(string s)
        {
            return JsonConvert.DeserializeObject<PlayerAction>(s);
        }


    }
}
