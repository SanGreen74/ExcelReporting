using Newtonsoft.Json;

namespace ExcelReporting.Client
{
    internal class JsonCustomSerializer
    {
        internal static readonly JsonCustomSerializer Instance = new JsonCustomSerializer();

        private readonly JsonSerializerSettings settings = JsonSerialization.CreateSettings();
        
        private JsonCustomSerializer()
        {
        }

        public string Serialize<T>(T value) => JsonConvert.SerializeObject(value, settings);

        public T Deserialize<T>(string value) => JsonConvert.DeserializeObject<T>(value, settings);
    }
}