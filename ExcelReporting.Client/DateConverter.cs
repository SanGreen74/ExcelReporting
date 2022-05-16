using System;
using Newtonsoft.Json;

namespace ExcelReporting.Client
{
    public class DateConverter : JsonConverter<Date>
    {
        public override void WriteJson(JsonWriter writer, Date value, JsonSerializer serializer)
        {
            var s = value.ToString();
            writer.WriteValue(s);
        }

        public override Date ReadJson(JsonReader reader, Type objectType, Date existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.String)
            {
                throw new JsonException($"Unable to read token \"{reader.TokenType}\" for Date");
            }

            var value = reader.Value as string;

            if (value == null)
            {
                return Date.MinValue;
            }
            
            if (!Date.TryParse(value, out var date))
            {
                throw new JsonException($"Unable to parse \"{value}\" to Date");
            }

            return date;
        }
    }
}