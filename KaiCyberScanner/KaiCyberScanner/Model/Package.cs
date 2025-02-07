using Newtonsoft.Json;
using Newtonsoft.Json;

namespace KaiCyberScanner.Model
{
    public class Package
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("fixedVersion")]
        public string FixedVersion { get; set; }
    }
}
