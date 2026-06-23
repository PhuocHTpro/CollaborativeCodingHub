using CollaborativeCodingClient.Protocol;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollaborativeCodingClient.Protocol
{
    public static class JsonHelper
    {
        public static string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static Packet Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<Packet>(json);
        }
    }
}
