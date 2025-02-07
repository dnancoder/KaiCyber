
using KaiCyberScanner.Database;
using KaiCyberScanner.Helper;
using KaiCyberScanner.Model;
using KaiCyberScanner.Processor;
using Newtonsoft.Json;

namespace KaiCyberScanner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                // Configure Newtonsoft.Json settings here if needed
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                // Example: Setting date format
                options.SerializerSettings.DateFormatString = "yyyy-MM-ddTHH:mm:ssZ";
            });

            //.AddNewtonsoftJson(options =>
            //{
            //    // Configure Newtonsoft.Json settings here if needed
            //    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            //};

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSingleton<IHttpClientHelper, HttpClientHelper>();
            builder.Services.AddTransient<IProcessor<List<ScanPayloadMetadata>>, ScanProcessor>();
            builder.Services.AddTransient<IProcessor<List<Vulnerability>>, QueryProcessor>();

            // Register the DatabaseInitializer
            builder.Services.AddHostedService<DatabaseInitializer>();

            builder.Services.AddSingleton<IDatabaseHelper, DatabaseHelper>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                return new DatabaseHelper(connectionString);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
