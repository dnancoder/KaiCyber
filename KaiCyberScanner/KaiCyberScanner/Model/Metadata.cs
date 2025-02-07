using Newtonsoft.Json;
using Newtonsoft.Json;

namespace KaiCyberScanner.Model
{
    public class Metadata
    {
        [JsonProperty("wizVersion")]
        public string WizVersion { get; set; }

        [JsonProperty("securityDefinitions")]
        public string SecurityDefinitions { get; set; }

        [JsonProperty("scanConfiguration")]
        public ScanConfiguration ScanConfiguration { get; set; }

        [JsonProperty("complianceFrameworks")]
        public List<string> ComplianceFrameworks { get; set; }
    }
}
