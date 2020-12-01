using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Cosmos;

namespace Recipe.Functions
{
    public class GetRecipeHttpTrigger
    {
        private readonly Container _recipeContainer;
        public GetRecipeHttpTrigger(Container recipeContainer)
        {
            _recipeContainer = recipeContainer;
        }

        [FunctionName("GetRecipe")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "recipe/{id}")] HttpRequest req, Guid id,
            ILogger log)
        {
            Models.Recipe result = await _recipeContainer.ReadItemAsync<Models.Recipe>(id.ToString(), new PartitionKey(id.ToString()));

            return new OkObjectResult(result);
        }
    }
}
