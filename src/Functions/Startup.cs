using System;
using System.Linq;
using AddQuoteFunction.Models;
using Functions;
using Functions.Models;
using Functions.TableServices;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // ------------------ default configuration initialise ------------------
            var serviceConfig = builder.Services.FirstOrDefault(s => s.ServiceType == typeof(IConfiguration));
            if (serviceConfig != null)
            {
                _ = (IConfiguration) serviceConfig.ImplementationInstance;
            }

            var config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();


            builder.Services.AddLogging();

            // ------------------ TableStorageDb initialise ------------------
            ITableConfiguration tableConfig = new TableConfiguration
            {
                ConnectionString = Environment.GetEnvironmentVariable("TableStorage-ConnectionString"),
                TableName = Environment.GetEnvironmentVariable("TableStorage-TableName")
            };

            builder.Services.AddSingleton(tableConfig);
            builder.Services.AddSingleton<ITableDbContext, TableDbContext>();

        }
    }
}