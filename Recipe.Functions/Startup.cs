using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(Recipe.Functions.Startup))]
namespace Recipe.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            builder.Services.AddSingleton(new CosmosClient(config.GetValue<string>("CosmosEndpointUrl"), config.GetValue<string>("CosmosEndpointPrimaryKey")));
            builder.Services.AddSingleton(x => x.GetService<CosmosClient>().GetContainer(config.GetValue<string>("CosmosDatabaseId"), config.GetValue<string>("CosmosContainerName")));
        }
    }
}

