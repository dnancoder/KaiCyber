using Newtonsoft.Json;
using Newtonsoft.Json;

namespace KaiCyberScanner.Model
{
    public class SeverityBreakdown
    {
        [JsonProperty("CRITICAL")]
        public int CRITICAL { get; set; }

        [JsonProperty("HIGH")]
        public int HIGH { get; set; }

        [JsonProperty("MEDIUM")]
        public int MEDIUM { get; set; }

        [JsonProperty("LOW")]
        public int LOW { get; set; }
    }
}
