using Newtonsoft.Json;

namespace KaiCyberScanner.Model
{
    public class ScanPayloadMetadata
    {
        [JsonProperty("id")]
        [JsonIgnore]
        public string Id { get; set; }

        [JsonProperty("source_file")]
        public string SourceFile { get; set; }

        [JsonProperty("scan_time")]
        public DateTimeOffset ScanTime { get; set; }

        [JsonProperty("payload")]
        public string Payload { get; set; }
    }
}
