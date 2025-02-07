using KaiCyberScanner.Model;

namespace KaiCyberScanner.Database
{
    public interface IDatabaseHelper
    {
        Task InsertAsync(Vulnerability vulnerability);
        Task UpdateAsync(Vulnerability vulnerability);
        Task UpsertAsync(Vulnerability vulnerability);
        Task<Vulnerability> GetByIdAsync(string id);
        Task<List<Vulnerability>> GetAllAsync(FilterQuery filterQuery);
        Task InsertAsync(ScanPayloadMetadata metadata);
    }
}
