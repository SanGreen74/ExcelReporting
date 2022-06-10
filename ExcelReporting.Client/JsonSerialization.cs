using Newtonsoft.Json;

namespace ExcelReporting.Client
{
    internal class JsonSerialization
    {
        public static JsonSerializerSettings CreateSettings()
        {
            var settings = new JsonSerializerSettings();
            return settings;
        }
    }
}