using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Configuration;
using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Cosmos;

namespace Recipe.Functions
{
    public static class CreateRecipeHttpTrigger
    {
        [FunctionName("CreateRecipeHttpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var cosmosClient = new CosmosClient(Environment.GetEnvironmentVariable("CosmosEndpointUrl"), Environment.GetEnvironmentVariable("CosmosEndpointPrimaryKey"));
            var database = (await cosmosClient.CreateDatabaseIfNotExistsAsync(Environment.GetEnvironmentVariable("CosmosDatabaseId"))).Database;
            var container = (await database.CreateContainerIfNotExistsAsync(Environment.GetEnvironmentVariable("CosmosContainerName"), "/id")).Container;

            var recipe = JsonConvert.DeserializeObject<Models.Recipe>(await new StreamReader(req.Body).ReadToEndAsync());

            ItemResponse<Models.Recipe> createResponse = await container.CreateItemAsync(recipe, new PartitionKey(recipe.Id.ToString()));

            return new OkObjectResult(recipe);
        }
    }
}
