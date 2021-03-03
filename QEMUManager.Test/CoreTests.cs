using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

using QEMUManager.Converters;
using QEMUManager.Helpers;
using QEMUManager.Models;

namespace QEMUManager.Test
{
    public class CoreTests
    {
        private ITestOutputHelper OutputHelper { get; }

        public CoreTests(ITestOutputHelper outputHelper)
        {
            this.OutputHelper = outputHelper;
        }

        [Fact]
        public void LoadSupportArchitectures()
        {
            var architectures = IOHelper.LoadSupportedArchitectures();

            Assert.True(architectures is IReadOnlyDictionary<String, QEMUArchitecture>);
            Assert.NotEmpty(architectures);
        }

        [Theory]
        [InlineData("i386")]
        [InlineData("x86_64")]
        public void ConvertArchitectureToJSON(String systemCode)
        {
            var architectures = IOHelper.LoadSupportedArchitectures();

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            JsonSerializer serializer = new JsonSerializer();

            serializer.Converters.Add(new QEMUArchitectureJSONConverter());

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;

                serializer.Serialize(writer, architectures[systemCode]);
            }

            Assert.True(sb.Length > 0);
            Assert.Contains(systemCode, sb.ToString());

            this.OutputHelper.WriteLine("JSON Contents:" + Environment.NewLine);
            this.OutputHelper.WriteLine(sb.ToString());
        }

        [Theory]
        [InlineData("\"i386\"")]
        [InlineData("\"x86_64\"")]
        public void ConvertArchitectureFromJSON(String systemCodeJSON)
        {
            JsonTextReader reader = new JsonTextReader(new StringReader(systemCodeJSON));
            JsonSerializer serializer = new JsonSerializer();

            serializer.Converters.Add(new QEMUArchitectureJSONConverter());

            var architecture = serializer.Deserialize<QEMUArchitecture>(reader);

            Assert.NotNull(architecture);
            Assert.Equal(architecture.Identifier, systemCodeJSON.Replace("\"", String.Empty));

            this.OutputHelper.WriteLine($"QEMU Identifier: {architecture.Identifier}");
        }
    }
}