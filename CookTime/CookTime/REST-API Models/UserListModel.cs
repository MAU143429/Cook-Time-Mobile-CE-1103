﻿
namespace CookTime.REST_API_UserListModel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json.Converters;
    using CookTime.REST_API_RecipeListModel;
    /// <summary>
    /// Class to convert a singly list of users into an object
    /// author Jose Antonio Espinoza.
    /// </summary>
    public partial class UserListModel
    {
        [JsonProperty("length")]
        public int Length { get; set; }

        [JsonProperty("head")]
        public Head Head { get; set; }

        [JsonProperty("last")]
        public Head Last { get; set; }
    }
    /// <summary>
    /// Class that creates next objects every time the next key is encountered
    /// author Jose Antonio Espinoza.
    /// </summary>
    public partial class Next
    {
        [JsonProperty("data")]
        public CookTime.REST_API_UserModel.User Data { get; set; }

        [JsonProperty("next")]
        public Head NextNext { get; set; }
    }
    /// <summary>
    /// Class that creates Head objects every time head key is encountered
    /// author Jose Antonio Espinoza.
    /// </summary>
    public partial class Head
    {
        [JsonProperty("data")]
        public CookTime.REST_API_UserModel.User Data { get; set; }

        [JsonProperty("next")]
        public Next Next { get; set; }
    }
    /// <summary>
    /// Class that creates User objects everytime "user" key is encountered in  the json file.
    /// author Jose Antonio Espinoza.
    /// </summary>
    public partial class Data
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("followers")]
        public JArray Followers { get; set; }

        [JsonProperty("following")]
        public JArray Following { get; set; }

        [JsonProperty("posts")]
        public int Posts { get; set; }

        [JsonProperty("recipes")]
        public JArray Recipes { get; set; }

        [JsonProperty("hascompany")]
        public Boolean Hascompany { get; set; }

        [JsonProperty("ischef")]
        public Boolean Ischef { get; set; }

        [JsonProperty("hasNotification")]
        public Boolean hasNotification { get; set; }
    }

    public partial class UserListModel
    {
        /// <summary>
        /// Methods that converts a json file to an object type
        /// author Jose Antonio Espinoza.
        /// </summary>
        public static UserListModel FromJson(string json) => JsonConvert.DeserializeObject<UserListModel>(json, CookTime.REST_API_UserListModel.Converter.Settings);
    }

    public static class Serialize
    {
        /// <summary>
        /// Methods that converts an object type into a json file
        /// author Jose Antonio Espinoza.
        /// </summary>
        public static string ToJson(this UserListModel self) => JsonConvert.SerializeObject(self, CookTime.REST_API_UserListModel.Converter.Settings);
    }
    /// <summary>
    /// Methods autogenerated by Newtonsoft.json
    /// author Jose Antonio Espinoza.
    /// </summary>
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
        /// <summary>
        /// Creates a singleton instance for the string parser.
        /// author Jose Antonio Espinoza.
        /// </summary>
        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
