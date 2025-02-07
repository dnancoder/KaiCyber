using KaiCyberScanner.Model;
using Microsoft.Data.Sqlite;
using System.Text;

namespace KaiCyberScanner.Database
{
    public class DatabaseHelper : IDatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task InsertAsync(Vulnerability vulnerability)
        {
            await InsertAsync(vulnerability);
        }

        public async Task UpdateAsync(Vulnerability vulnerability)
        {
            await InsertAsync(vulnerability);
        }

        public async Task UpsertAsync(Vulnerability vulnerability)
        {
            var utcNowOffset = DateTimeOffset.UtcNow;
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO Vulnerabilities (id, severity, cvss, status, package_name, current_version, fixed_version, description, published_date, link, risk_factors, created_datetime, updated_datetime)
                    VALUES (@id, @severity, @cvss, @status, @package_name, @current_version, @fixed_version, @description, @published_date, @link, @risk_factors, @created_datetime, @updated_datetime)
                    ON CONFLICT(id) DO UPDATE SET
                        severity = excluded.severity,
                        cvss = excluded.cvss,
                        status = excluded.status,
                        package_name = excluded.package_name,
                        current_version = excluded.current_version,
                        fixed_version = excluded.fixed_version,
                        description = excluded.description,
                        published_date = excluded.published_date,
                        link = excluded.link,
                        risk_factors = excluded.risk_factors,
                        updated_datetime = excluded.updated_datetime;
                ";

                command.Parameters.AddWithValue("@id", vulnerability.Id);
                command.Parameters.AddWithValue("@severity", vulnerability.Severity ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@cvss", vulnerability.Cvss);
                command.Parameters.AddWithValue("@status", vulnerability.Status ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@package_name", vulnerability.PackageName ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@current_version", vulnerability.CurrentVersion ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@fixed_version", vulnerability.FixedVersion ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@description", vulnerability.Description ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@published_date", vulnerability.PublishedDate.ToString("o"));
                command.Parameters.AddWithValue("@link", vulnerability.Link ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@risk_factors", vulnerability.RiskFactors != null? string.Join(",", vulnerability.RiskFactors) : (object)DBNull.Value);
                command.Parameters.AddWithValue("@created_datetime", utcNowOffset.ToString("o"));
                command.Parameters.AddWithValue("@updated_datetime", utcNowOffset.ToString("o"));

                await command.ExecuteNonQueryAsync();
            }
        }


        public async Task<Vulnerability> GetByIdAsync(string id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = @"
                    SELECT id, severity, cvss, status, package_name, current_version, fixed_version, description, published_date, link, risk_factors
                    FROM Vulnerabilities
                    WHERE id = @id;
                ";
                command.Parameters.AddWithValue("@id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Vulnerability
                        {
                            Id = reader.GetString(0),
                            Severity = reader.GetString(1),
                            Cvss = reader.GetDouble(2),
                            Status = reader.GetString(3),
                            PackageName = reader.GetString(4),
                            CurrentVersion = reader.GetString(5),
                            FixedVersion = reader.GetString(6),
                            Description = reader.GetString(7),
                            PublishedDate = DateTimeOffset.Parse(reader.GetString(8)),
                            Link = reader.GetString(9),
                            RiskFactors = reader.GetString(10).Split(',').ToList()
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public async Task<List<Vulnerability>> GetAllAsync(FilterQuery filterQuery)
        {
            var vulnerabilities = new List<Vulnerability>();
            var whereCondition = await this.GetQuery(filterQuery.Filters);
            try
            {

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                    command.CommandText = @"
                    SELECT id, severity, cvss, status, package_name, current_version, fixed_version, description, published_date, link, risk_factors
                    FROM Vulnerabilities" + whereCondition;
                await this.SetQueryParameters(command, filterQuery.Filters);


                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        vulnerabilities.Add(new Vulnerability
                        {
                            Id = reader.GetString(0),
                            Severity = reader.GetString(1),
                            Cvss = reader.GetDouble(2),
                            Status = reader.GetString(3),
                            PackageName = reader.GetString(4),
                            CurrentVersion = reader.GetString(5),
                            FixedVersion = reader.GetString(6),
                            Description = reader.GetString(7),
                            PublishedDate = DateTimeOffset.Parse(reader.GetString(8)),
                            Link = reader.GetString(9),
                            RiskFactors = reader.GetString(10).Split(',').ToList()
                        });
                    }
                }
            }
            }catch (SqliteException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return vulnerabilities;
        }

        public async Task InsertAsync(ScanPayloadMetadata metadata)
        {
            var utcNowOffset = DateTimeOffset.UtcNow;
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO ScanPaylodMetadata (SourceFile, ScanTime, PayloadData, created_datetime)
                    VALUES (@sourceFile, @scanTime, @payloadData, @created_datetime)
                    ";

                command.Parameters.AddWithValue("@sourceFile", metadata.SourceFile ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@scanTime", metadata.ScanTime.ToString("o"));
                command.Parameters.AddWithValue("@payloadData", metadata.Payload ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@created_datetime", utcNowOffset.ToString("o"));

                await command.ExecuteNonQueryAsync();
            }
        }

        private async Task<string> GetQuery(Filters filters)
        {
            var sbFilter = new StringBuilder(" WHERE 1=1");

            if (!string.IsNullOrEmpty(filters.Severity))
                sbFilter.Append(" AND severity = @severity");

            if (!string.IsNullOrEmpty(filters.Source))
                sbFilter.Append(" AND source = @source");

            if (!string.IsNullOrEmpty(filters.Category))
                sbFilter.Append(" AND category = @category");

            if (filters.DateRange != null)
            {
                if (filters.DateRange.StartDate.HasValue)
                    sbFilter.Append(" AND created_datetime >= @start_date");

                if (filters.DateRange.EndDate.HasValue)
                    sbFilter.Append(" AND created_datetime <= @end_date");
            }

            return sbFilter.ToString();
        }

        private async Task SetQueryParameters(SqliteCommand command, Filters filters)
        {
            if (!string.IsNullOrEmpty(filters.Severity))
                command.Parameters.AddWithValue("@severity", filters.Severity);

            if (!string.IsNullOrEmpty(filters.Source))
                command.Parameters.AddWithValue("@source", filters.Source);

            if (!string.IsNullOrEmpty(filters.Category))
                command.Parameters.AddWithValue("@category", filters.Category);

            if (filters.DateRange != null)
            {
                if (filters.DateRange.StartDate.HasValue)
                    command.Parameters.AddWithValue("@start_date", filters.DateRange.StartDate.Value.ToString("o")); // ISO 8601 format

                if (filters.DateRange.EndDate.HasValue)
                    command.Parameters.AddWithValue("@end_date", filters.DateRange.EndDate.Value.ToString("o")); // ISO 8601 format
            }
        }
    }
}
