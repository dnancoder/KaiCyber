using Newtonsoft.Json;

namespace KaiCyberScanner.Model
{
    public class Root
    {
        [JsonProperty("scanResults")]
        public ScanResults ScanResults { get; set; }
    }
}
