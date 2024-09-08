using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;

namespace PBASE.WebAPI.Helpers
{
    class GBPDateFormatter : DateTimeConverterBase
    {
        /// <summary>
        /// DateTime format
        /// </summary>
        private const string Format = "dd/MM/yyyy";
        private const string TimeFormat = "dd/MM/yyyy HH:mm";

        /// <summary>
        /// Writes value to JSON
        /// </summary>
        /// <param name="writer">JSON writer</param>
        /// <param name="value">Value to be written</param>
        /// <param name="serializer">JSON serializer</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            DateTime d = (DateTime)value;
            if (d.TimeOfDay.Ticks > 0)
            {
                writer.WriteValue(d.ToString(TimeFormat));
            }
            else
            {
                writer.WriteValue(d.ToString(Format));
            }
        }

        /// <summary>
        /// Reads value from JSON
        /// </summary>
        /// <param name="reader">JSON reader</param>
        /// <param name="objectType">Target type</param>
        /// <param name="existingValue">Existing value</param>
        /// <param name="serializer">JSON serialized</param>
        /// <returns>Deserialized DateTime</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return null;
            }

            var s = reader.Value.ToString();
            DateTime result;
            if (DateTime.TryParseExact(s, Format, CultureInfo.CurrentCulture, DateTimeStyles.None, out result))
            {
                return result.Date;
            }

            if (DateTime.TryParse(s, out result))
            {
                if (result.TimeOfDay.Hours > 0)
                {
                    return result;
                }
                else
                {
                    return result.Date;
                }
            }

            return DateTime.Now.Date;
        }

    }
}
