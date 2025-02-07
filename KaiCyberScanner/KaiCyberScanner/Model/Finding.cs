using Newtonsoft.Json;
using Newtonsoft.Json;

namespace KaiCyberScanner.Model
{
    public class Finding
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("cveId")]
        public string CveId { get; set; }

        [JsonProperty("severity")]
        public string Severity { get; set; }

        [JsonProperty("score")]
        public double Score { get; set; }

        [JsonProperty("package")]
        public Package Package { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("remediation")]
        public string Remediation { get; set; }

        [JsonProperty("firstDetected")]
        public DateTime FirstDetected { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("exploitability")]
        public string Exploitability { get; set; }

        [JsonProperty("threatContext")]
        public ThreatContext ThreatContext { get; set; }
    }
}
