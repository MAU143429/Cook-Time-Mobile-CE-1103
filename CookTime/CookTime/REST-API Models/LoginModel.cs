﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using CookTime.REST_API_Models;
//
//    var registrationModel = RegistrationModel.FromJson(jsonString);

namespace CookTime.REST_API_LoginModel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class LoginModel
    {
        [JsonProperty("users")]
        public User[] Users { get; set; }
    }

    public partial class User
    {
       
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

    }

    public partial class LoginModel
    {
        public static LoginModel FromJson(string json) => JsonConvert.DeserializeObject<LoginModel>(json, CookTime.REST_API_LoginModel.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this LoginModel self) => JsonConvert.SerializeObject(self, CookTime.REST_API_LoginModel.Converter.Settings);
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
