using Newtonsoft.Json;
using Newtonsoft.Json;

namespace KaiCyberScanner.Model
{
    public class ScanConfiguration
    {
        [JsonProperty("enabledChecks")]
        public List<string> EnabledChecks { get; set; }

        [JsonProperty("excludedPaths")]
        public List<string> ExcludedPaths { get; set; }
    }
}
