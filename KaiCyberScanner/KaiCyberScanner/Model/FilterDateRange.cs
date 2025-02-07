using Newtonsoft.Json;

namespace KaiCyberScanner.Model
{
    public class FilterDateRange
    {
        [JsonProperty("start_date")]
        public DateTimeOffset? StartDate { get; set; }

        [JsonProperty("end_date")]
        public DateTimeOffset? EndDate { get; set; }
    }
}
