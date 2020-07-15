using System;
using System.Collections.Generic;
using System.Text;

namespace CookTime.REST_API_RecipeListModel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json.Converters;

    public partial class RecipeListModel
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
        public CookTime.REST_API_RecipeModel.Recipe Data { get; set; }

        [JsonProperty("next")]
        public Head NextNext { get; set; }
    }

    public partial class Head
    {
        [JsonProperty("data")]
        public CookTime.REST_API_RecipeModel.Recipe Data { get; set; }

        [JsonProperty("next")]
        public Next Next { get; set; }
    }

    public partial class Data
    {
      
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("type")]
        public string TypeOfDish { get; set; }

        [JsonProperty("servings")]
        public int Servings { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("difficulty")]
        public int Difficulty { get; set; }

        [JsonProperty("diet")]
        public string Diet { get; set; }

        [JsonProperty("ingredients")]
        public string Ingredients { get; set; }

        [JsonProperty("steps")]
        public string Steps { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("image")]
        public string ImageURL { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("rating")]
        public int Rating { get; set; }

        [JsonProperty("comments")]
        public JArray Comments { get; set; }


    }

        public partial class RecipeListModel
    {
        public static RecipeListModel FromJson(string json) => JsonConvert.DeserializeObject<RecipeListModel>(json, CookTime.REST_API_RecipeListModel.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this RecipeListModel self) => JsonConvert.SerializeObject(self, CookTime.REST_API_RecipeListModel.Converter.Settings);
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
