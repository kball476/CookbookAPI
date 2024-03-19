using cookbook3.Models;
using cookbook3.Data;

namespace cookbook3
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if(!dataContext.RecipeCategories.Any())
            {
                var recipeCategories = new List<RecipeCategory>()
                {
                    new RecipeCategory()
                    {
                        Recipe = new Recipe()
                        {
                            Name = "Pb&J",
                            Description = "A soft, sweet snack to-go",
                            Owner = new Owner()
                            {
                               Name = "Chef" 
                            },
                            Link = "www.howtomakepbj.com",

                            Reviews = new List<Review>()
                            {
                                new Review {Rating = 4, Text = "Too sweet, no flavor",
                                    Reviewer = new Reviewer(){Name = "pb&J Hater"}},
                                new Review { Rating = 10, Text = "My favorite snack",
                                    Reviewer = new Reviewer(){Name = "Foodie"}},
                            }

                        },
                        Category = new Category()
                            {
                               Type = "Snack",
                               
                            }
                    }

                };
                dataContext.RecipeCategories.AddRange(recipeCategories);
                dataContext.SaveChanges();
            }
        }
    }
}
