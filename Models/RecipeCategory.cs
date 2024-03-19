namespace cookbook3.Models
{
    public class RecipeCategory
    {
        public int RecipeId { get; set; }

        public int CategoryId { get; set; }

        public Recipe Recipe { get; set; }

        public Category Category { get; set; }
    }
}
