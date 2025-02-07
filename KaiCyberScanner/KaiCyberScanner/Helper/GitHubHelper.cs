using Octokit;

namespace KaiCyberScanner.Helper
{
    public class GitHubHelper
    {
        private readonly GitHubClient client;

        public GitHubHelper()
        {
            client = new GitHubClient(new ProductHeaderValue("MyApp"));
        }

        public async Task<List<string>> GetFileUrlsAsync(string owner, string repo)
        {
            var urls = new List<string>();
            var contents = await client.Repository.Content.GetAllContents(owner, repo);

            foreach (var file in contents)
            {
                if (file.Type == ContentType.File)
                {
                    urls.Add(file.DownloadUrl);
                }
            }

            return urls;
        }

        public async Task<string> DownloadFileAsync(string url, string downloadPath)
        {
            var fileName = Path.GetFileName(new Uri(url).LocalPath);
            var fullPath = Path.Combine(downloadPath, fileName);

            using (var httpClient = new HttpClient())
            {
                var fileContent = await httpClient.GetByteArrayAsync(url);
                await File.WriteAllBytesAsync(fullPath, fileContent);
            }

            return fullPath;
        }
    }
}
