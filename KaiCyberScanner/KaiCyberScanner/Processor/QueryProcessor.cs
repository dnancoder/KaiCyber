using KaiCyberScanner.Database;
using KaiCyberScanner.Helper;
using KaiCyberScanner.Model;

namespace KaiCyberScanner.Processor
{
    public class QueryProcessor : IProcessor<List<Vulnerability>>
    {
        private readonly IDatabaseHelper _databaseHelper;
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly GitHubHelper _gitHubHelper;

        public QueryProcessor(IDatabaseHelper databaseHelper, IHttpClientHelper httpClientHelper)
        {
            this._databaseHelper = databaseHelper;
            this._httpClientHelper = httpClientHelper;
            this._gitHubHelper = new GitHubHelper();
        }

        public async Task<List<Vulnerability>> ProcessAsync(IModelBase modelBase)
        {
            var filterQuery = (FilterQuery) modelBase;
            var vulnerabilityList = await _databaseHelper.GetAllAsync(filterQuery);
            return vulnerabilityList;
        }
    }
}
