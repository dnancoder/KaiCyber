using Newtonsoft.Json;
using Newtonsoft.Json;

namespace KaiCyberScanner.Model
{
    public class ScanResults
    {
        [JsonProperty("scan_id")]
        //[JsonProperty("scanId")]
        public string ScanId { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("scan_status")]
        public string ScanStatus { get; set; }

        [JsonProperty("resource_type")]
        public string ResourceType { get; set; }

        [JsonProperty("resource_name")]
        public string ResourceName { get; set; }

        [JsonProperty("vulnerabilities")]
        public List<Vulnerability> Vulnerabilities { get; set; }

        [JsonProperty("summary")]
        public Summary Summary { get; set; }

        [JsonProperty("scan_metadata")]
        public ScanMetadata ScanMetadata { get; set; }

        [JsonProperty("scanId")]
        public string ScanIdAlias { get; set; }

        [JsonProperty("scanTime")]
        public DateTime? ScanTime { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("resourceDetails")]
        public ResourceDetails ResourceDetails { get; set; }

        [JsonProperty("findings")]
        public List<Finding> Findings { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }
    }

}
