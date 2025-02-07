namespace KaiCyberScanner.Helper
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class HttpClientHelper: IHttpClientHelper
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<string> GetAsync(string url)
        {
            const int maxRetries = 3;
            int retryCount = 0;
            while (true)
            {
                try
                {
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException ex) when (retryCount < maxRetries)
                {
                    retryCount++;
                    Console.WriteLine($"Request failed ({ex.Message}). Retrying... ({retryCount}/{maxRetries})");
                    await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, retryCount)));
                }
            }
        }
    }

}
