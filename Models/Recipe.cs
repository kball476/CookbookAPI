namespace cookbook3.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public string Description { get; set; }

        public string Link { get; set; }


        public Owner Owner { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<RecipeCategory> RecipeCategories { get; set; }
    }
}
