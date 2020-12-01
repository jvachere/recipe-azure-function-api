using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos;

namespace Recipe.Functions
{
    public class PostRecipeHttpTrigger
    {
        private readonly Container _recipeContainer;
        public PostRecipeHttpTrigger(Container recipeContainer)
        {
            _recipeContainer = recipeContainer;
        }

        [FunctionName("PostRecipe")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "recipe")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var recipe = JsonConvert.DeserializeObject<Models.Recipe>(await new StreamReader(req.Body).ReadToEndAsync());

            Models.Recipe result = await _recipeContainer.CreateItemAsync(recipe, new PartitionKey(recipe.Id.ToString()));

            return new OkObjectResult(result);
        }
    }
}
