namespace Recipe.Functions.Models
{
    public class RecipeIngredient
    {
        public int Ordinal { get; set; }
        public string Name { get; set; }
        public decimal? Quantity { get; set; }
        public string Unit { get; set; }
        public bool IsOptional { get; set; }
        public string Note { get; set; }
    }
}
