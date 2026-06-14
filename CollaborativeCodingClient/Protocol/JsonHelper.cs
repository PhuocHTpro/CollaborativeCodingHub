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
        public static string Serialize(Packet packet)
        {
            return JsonConvert.SerializeObject(packet);
        }

        public static Packet Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<Packet>(json);
        }
    }
}
