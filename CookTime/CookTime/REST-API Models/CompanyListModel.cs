using System;
using System.Collections.Generic;
using System.Text;

namespace CookTime.REST_API_CompanyListModel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Linq;

    public partial class CompanyListModel
    {
        [JsonProperty("length")]
        public int Length { get; set; }

        [JsonProperty("head")]
        public Head Head { get; set; }

        [JsonProperty("last")]
        public Head Last { get; set; }
    }

    public partial class Next
    {
        [JsonProperty("data")]
        public CookTime.REST_API_CompanyModel.Company Data { get; set; }

        [JsonProperty("next")]
        public Head NextNext { get; set; }
    }

    public partial class Head
    {
        [JsonProperty("data")]
        public CookTime.REST_API_CompanyModel.Company Data { get; set; }

        [JsonProperty("next")]
        public Next Next { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("schedule")]
        public string Schedule { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("posts")]
        public int Posts { get; set; }

        [JsonProperty("followers")]
        public JArray Followers { get; set; }

        [JsonProperty("following")]
        public JArray Following { get; set; }

        [JsonProperty("members")]
        public JArray Members { get; set; }
    }

    public partial class CompanyListModel
    {
        public static CompanyListModel FromJson(string json) => JsonConvert.DeserializeObject<CompanyListModel>(json, CookTime.REST_API_CompanyListModel.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this CompanyListModel self) => JsonConvert.SerializeObject(self, CookTime.REST_API_CompanyListModel.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
