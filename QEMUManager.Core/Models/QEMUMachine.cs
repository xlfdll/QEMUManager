using System;

using Newtonsoft.Json;

using QEMUManager.Converters;

namespace QEMUManager.Models
{
    public class QEMUMachine
    {
        [JsonConstructor]
        public QEMUMachine(String name, QEMUArchitecture archtecture)
        {
            this.Name = name;
            this.Architecture = archtecture;
        }

        public String Name { get; set; }
        [JsonConverter(typeof(QEMUArchitectureJSONConverter))]
        public QEMUArchitecture Architecture { get; }
        public String AdditionalParameters { get; set; }
    }
}