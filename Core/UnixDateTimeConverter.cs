﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace MangoPay.SDK.Core
{
    /// <summary>
    /// Converter between .NET DateTime and unix datetime. This one puts NULL as a default value.
    /// </summary>
    public class UnixDateTimeConverter : DateTimeConverterBase
    {
        internal long? ConvertToUnixFormat(DateTime? dateTime)
        {
            if (dateTime == null) return null;

            return (long?)(dateTime.Value - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
        }

        internal DateTime? ConvertFromUnixFormat(long? unixTime)
        {
            if (unixTime == null) return null;

            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (DateTime?)start.AddSeconds(unixTime.Value);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            long result;
            if (value is DateTime)
            {
                long? ticks = ConvertToUnixFormat((DateTime)value);
                result = ticks.Value;
            }
            else
            {
                throw new Exception("Expected date object value.");
            }
            writer.WriteValue(result);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            object result = null;
            if (reader.TokenType == JsonToken.Integer)
            {
                result = ConvertFromUnixFormat((long)reader.Value);
            }
            else if (reader.TokenType == JsonToken.Float)
            {
                // TODO API V2 Correct me
                result = ConvertFromUnixFormat(long.Parse(reader.Value.ToString()));
            }

            return result;

        }
    }
}
