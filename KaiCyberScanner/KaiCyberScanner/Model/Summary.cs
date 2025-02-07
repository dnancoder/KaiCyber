using Newtonsoft.Json;
using Newtonsoft.Json;

namespace KaiCyberScanner.Model
{
    public class Summary
    {
        [JsonProperty("total_vulnerabilities")]
        public int TotalVulnerabilities { get; set; }

        [JsonProperty("severity_counts")]
        public SeverityCounts SeverityCounts { get; set; }

        [JsonProperty("fixable_count")]
        public int FixableCount { get; set; }

        [JsonProperty("compliant")]
        public bool Compliant { get; set; }

        [JsonProperty("totalIssues")]
        public int? TotalIssues { get; set; }

        [JsonProperty("severityBreakdown")]
        public SeverityBreakdown SeverityBreakdown { get; set; }

        [JsonProperty("fixableIssues")]
        public int? FixableIssues { get; set; }

        [JsonProperty("securityScore")]
        public int? SecurityScore { get; set; }
    }
}
