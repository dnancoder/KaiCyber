using Newtonsoft.Json;
using Newtonsoft.Json;

namespace KaiCyberScanner.Model
{
    public class ResourceDetails
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("registry")]
        public string Registry { get; set; }

        [JsonProperty("architecture")]
        public string Architecture { get; set; }
    }
}
