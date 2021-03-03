using System;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;

using Xlfdll.Text;

using QEMUManager.Core.Properties;
using QEMUManager.Models;

namespace QEMUManager.Helpers
{
    public static class IOHelper
    {
        public static IReadOnlyDictionary<String, QEMUArchitecture> LoadSupportedArchitectures()
        {
            Dictionary<String, QEMUArchitecture> architectures = new Dictionary<String, QEMUArchitecture>();
            String json = AdditionalEncodings.UTF8WithoutBOM.GetString(Resources.MachineArchitectures);
            JArray jsonArray = JArray.Parse(json);

            foreach (JToken token in jsonArray.Children())
            {
                String identifier = token["Identifier"]?.Value<String>();
                String description = token["Description"]?.Value<String>();

                architectures.Add(identifier, new QEMUArchitecture(identifier, description));
            }

            return architectures;
        }
    }
}