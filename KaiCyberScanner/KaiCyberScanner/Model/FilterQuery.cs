
namespace KaiCyberScanner.Model
{
    using Newtonsoft.Json;

    public class FilterQuery: IModelBase
    {
        [JsonProperty("filters")]
        public Filters? Filters { get; set; }
    }
}
