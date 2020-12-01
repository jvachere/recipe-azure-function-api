using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos;

namespace Recipe.Functions
{
    public class GetRecipesHttpTrigger
    {
        private readonly Container _recipeContainer;
        public GetRecipesHttpTrigger(Container recipeContainer)
        {
            _recipeContainer = recipeContainer;
        }

        [FunctionName("GetRecipes")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "recipes")] HttpRequest req,
            ILogger log)
        {
            FeedIterator<Models.Recipe> queryResultSetIterator = _recipeContainer.GetItemQueryIterator<Models.Recipe>("SELECT * FROM c");

            List<Models.Recipe> recipes = new List<Models.Recipe>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Models.Recipe> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                recipes.AddRange(currentResultSet);
            }

            return new OkObjectResult(recipes);
        }
    }
}
