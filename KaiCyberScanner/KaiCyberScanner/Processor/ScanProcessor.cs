using KaiCyberScanner.Database;
using KaiCyberScanner.Helper;
using KaiCyberScanner.Model;
using Newtonsoft.Json;

namespace KaiCyberScanner.Processor
{
    public class ScanProcessor : IProcessor<List<ScanPayloadMetadata>>
    {
        private readonly IDatabaseHelper _databaseHelper;
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly GitHubHelper _gitHubHelper;

        public ScanProcessor(IDatabaseHelper databaseHelper, IHttpClientHelper httpClientHelper)
        {
            this._databaseHelper = databaseHelper;
            this._httpClientHelper = httpClientHelper;
            this._gitHubHelper = new GitHubHelper();
        }

        public async Task<List<ScanPayloadMetadata>> ProcessAsync(IModelBase modelBase)
        {
            var scanRepo = (ScanRepo)modelBase;
            var repoUrlParts = scanRepo.RepoRoot.Split('/');
            var repoOwner = repoUrlParts[repoUrlParts.Length - 2];
            var repoName = repoUrlParts[repoUrlParts.Length - 1];
            var fileList = await _gitHubHelper.GetFileUrlsAsync(repoOwner, repoName);
            var scanPayloadMetadataList = new List<ScanPayloadMetadata>();
            var tasks = scanRepo.Files.Select(async fileName =>
            {
                var gitHubDownloadUrl = fileList.FirstOrDefault(fl => fl.EndsWith("/" + fileName));
                if (gitHubDownloadUrl != null)
                {
                    var fileUrl = "https://github.com/velancio/vulnerability_scans/blob/main/vulnscan1011.json";
                    var stringContent = await _httpClientHelper.GetAsync(gitHubDownloadUrl);

                    var scanPayloadMetadata = new ScanPayloadMetadata
                    {
                        ScanTime = DateTimeOffset.UtcNow,
                        SourceFile = gitHubDownloadUrl,
                        Payload = stringContent
                    };
                    await _databaseHelper.InsertAsync(scanPayloadMetadata);
                    scanPayloadMetadataList.Add(scanPayloadMetadata);

                    var rootList = JsonConvert.DeserializeObject<List<Root>>(stringContent);
                    if (rootList == null)
                    {
                        Console.WriteLine($"Failed to deserialize the content of file: {fileUrl}");
                    }
                    else
                    {
                        await UpsertScanResultsAsync(rootList);
                    }
                }
            });

            await Task.WhenAll(tasks);
            return scanPayloadMetadataList;
        }


        private async Task UpsertScanResultsAsync(List<Root> rootList)
        {
            foreach (var root in rootList)
            {
                foreach (var vulnerability in root.ScanResults.Vulnerabilities)
                {
                    await this._databaseHelper.UpsertAsync(vulnerability);
                }
            }
        }
    }
}
