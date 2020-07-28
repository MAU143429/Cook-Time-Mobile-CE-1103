﻿
namespace CookTime.REST_API_Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    /// <summary>
    /// Class that packs newuser objects (users for registering) into an array.
    /// author Jose Antonio Espinoza.
    /// </summary>
    public partial class RegistrationModel
    {
        [JsonProperty("users")]
        public Newuser[] Newusers { get; set; }
    }
    /// <summary>
    /// Class that makes Newuser objects from data entries and then can be converted into json files for communication
    /// author Jose Antonio Espinoza.
    /// </summary>
    public partial class Newuser
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }




    }

    public partial class RegistrationModel
    {
        /// <summary>
        /// Methods that converts a json file to an object type
        /// author Jose Antonio Espinoza.
        /// </summary>
        public static RegistrationModel FromJson(string json) => JsonConvert.DeserializeObject<RegistrationModel>(json, CookTime.REST_API_Models.Converter.Settings);
    }

    public static class Serialize
    {
        /// <summary>
        /// Methods that converts an object type into a json file
        /// author Jose Antonio Espinoza.
        /// </summary>
        public static string ToJson(this RegistrationModel self) => JsonConvert.SerializeObject(self, CookTime.REST_API_Models.Converter.Settings);
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
        /// Creates a unique singleton instance of the string parser.
        /// author Jose Antonio Espinoza.
        /// </summary>
        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}


