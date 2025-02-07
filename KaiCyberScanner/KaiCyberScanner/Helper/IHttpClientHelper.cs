namespace KaiCyberScanner.Helper
{
    public interface IHttpClientHelper
    {
        Task<string> GetAsync(string url);
    }
}
