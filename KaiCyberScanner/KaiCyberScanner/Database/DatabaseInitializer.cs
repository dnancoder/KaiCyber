using Microsoft.Data.Sqlite;

namespace KaiCyberScanner.Database
{
    public class DatabaseInitializer : IHostedService
    {
        private readonly IConfiguration _configuration;

        public DatabaseInitializer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Vulnerabilities (
                        id TEXT PRIMARY KEY,
                        severity TEXT NOT NULL,
                        cvss REAL NOT NULL,
                        status TEXT NOT NULL,
                        package_name TEXT NOT NULL,
                        current_version TEXT NOT NULL,
                        fixed_version TEXT NOT NULL,
                        description TEXT,
                        published_date TEXT,
                        link TEXT,
                        risk_factors TEXT,
                        created_datetime TEXT,
                        updated_datetime TEXT
                    );
                ";
                command.ExecuteNonQueryAsync();

                command.CommandText = @"
                    CREATE TABLE ScanPaylodMetadata (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        SourceFile TEXT NOT NULL,
                        ScanTime TEXT NOT NULL,
                        PayloadData TEXT,
                        created_datetime TEXT
                    );
                ";
                command.ExecuteNonQueryAsync();
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
