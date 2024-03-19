namespace cookbook3.Models
{
    public class Reviewer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
