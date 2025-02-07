using Newtonsoft.Json;
using Newtonsoft.Json;

namespace KaiCyberScanner.Model
{
    public class ScanMetadata
    {
        [JsonProperty("scanner_version")]
        public string ScannerVersion { get; set; }

        [JsonProperty("policies_version")]
        public string PoliciesVersion { get; set; }

        [JsonProperty("scanning_rules")]
        public List<string> ScanningRules { get; set; }

        [JsonProperty("excluded_paths")]
        public List<string> ExcludedPaths { get; set; }
    }
}
