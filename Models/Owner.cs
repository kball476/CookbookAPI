namespace cookbook3.Models
{
    public class Owner
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public ICollection<Recipe> Recipes { get; set; }
    }
}
