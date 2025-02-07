using Newtonsoft.Json;
using Newtonsoft.Json;

namespace KaiCyberScanner.Model
{
    public class ThreatContext
    {
        [JsonProperty("inTheWild")]
        public bool InTheWild { get; set; }

        [JsonProperty("hasExploit")]
        public bool HasExploit { get; set; }

        [JsonProperty("exploitMaturity")]
        public string ExploitMaturity { get; set; }
    }
}
