using Newtonsoft.Json;

namespace CollaborativeCodingClient.Network
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
