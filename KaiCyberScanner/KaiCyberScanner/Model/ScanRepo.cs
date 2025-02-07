namespace KaiCyberScanner.Model
{
    using Newtonsoft.Json;

    public class ScanRepo : IModelBase
    {
        [JsonProperty("repo")]
        public string RepoRoot { get; set; }

        [JsonProperty("files")]
        public List<string> Files { get; set; }
    }

}
