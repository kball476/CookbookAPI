namespace cookbook3.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Type { get; set; }
        //bfast, dinner, sweets. etc

        public ICollection<RecipeCategory> RecipeCategories { get; set; }

    }
}
