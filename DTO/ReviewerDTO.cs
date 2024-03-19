using cookbook3.Models;

namespace cookbook3.DTO
{
    public class ReviewerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
