using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Recipe.Functions.Models
{
    public class Recipe
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public string PrepTime { get; set; }
        public string BakeTime { get; set; }
        public string InactiveTime { get; set; }
        public List<string> Tags { get; set; }
        public decimal Quantity { get; set; }
        public string YieldString { get; set; }
        public List<RecipeStep> RecipeSteps { get; set; }
        public List<RecipeIngredient> RecipeIngredients { get; set; }
        public Recipe()
        {
            Id = Guid.NewGuid();
        }
    }
}
