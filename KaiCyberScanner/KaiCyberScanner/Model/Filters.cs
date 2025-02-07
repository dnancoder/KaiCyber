using Newtonsoft.Json;

namespace KaiCyberScanner.Model
{
    public class Filters
    {
        [JsonProperty("severity")]
        public string? Severity { get; set; }

        [JsonProperty("source")]
        public string? Source { get; set; }

        [JsonProperty("category")]
        public string? Category { get; set; }

        [JsonProperty("date_range")]
        public FilterDateRange? DateRange { get; set; }
    }
}
