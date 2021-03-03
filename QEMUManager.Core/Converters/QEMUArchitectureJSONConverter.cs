using System;

using Newtonsoft.Json;

using QEMUManager.Models;

namespace QEMUManager.Converters
{
    public class QEMUArchitectureJSONConverter : JsonConverter
    {
        public override Boolean CanConvert(Type objectType)
        {
            return objectType == typeof(QEMUArchitecture);
        }

        public override Object ReadJson(JsonReader reader, Type objectType, Object existingValue, JsonSerializer serializer)
        {
            QEMUArchitecture architecture = QEMUChecker.UnsupportedArchitecture;
            String identifier = reader.Value as String;

            if (!String.IsNullOrEmpty(identifier) && QEMUChecker.SupportedArchitectures.ContainsKey(identifier))
            {
                architecture = QEMUChecker.SupportedArchitectures[identifier];
            }

            return architecture;
        }

        public override void WriteJson(JsonWriter writer, Object value, JsonSerializer serializer)
        {
            QEMUArchitecture architecture = value as QEMUArchitecture;

            writer.WriteValue(architecture != null ? architecture.Identifier : String.Empty);
        }
    }
}