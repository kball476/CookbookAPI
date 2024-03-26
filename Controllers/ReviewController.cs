using AutoMapper;
using cookbook3.DTO;
using cookbook3.Interfaces;
using cookbook3.Models;
using cookbook3.Repository;
using Microsoft.AspNetCore.Mvc;

namespace cookbook3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IReviewerRepository _reviewerRepository;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper,
             IRecipeRepository recipeRepository, IReviewerRepository reviewerRepository)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _recipeRepository = recipeRepository;
            _reviewerRepository = reviewerRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetRecipes()
        {
            try
            {
                var reviews = _mapper.Map<List<ReviewDTO>>(_reviewRepository.GetReviews());

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(reviews);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            try
            {
                if (!_reviewRepository.ReviewExists(reviewId))
                    return NotFound();

                var review = _mapper.Map<ReviewDTO>(_reviewRepository.GetReview(reviewId));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(review);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("recipe/{recipeId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsForRecipe(int recipeId)
        {
            try
            {
                var reviews = _mapper.Map<List<ReviewDTO>>(_reviewRepository.GetReviewsOfRecipe(recipeId));

                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(reviews);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery] int reviewerId, [FromQuery] int recipeId, [FromBody] ReviewDTO reviewCreate)
        {
            try
            {
                if (reviewCreate == null)
                    return BadRequest(ModelState);

                var reviews = _reviewRepository.GetReviews()
                    .Where(c => c.Rating == reviewCreate.Rating)
                    .FirstOrDefault();

                if (reviews != null)
                {
                    ModelState.AddModelError("", "Review already exists");
                    return StatusCode(422, ModelState);

                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var reviewMap = _mapper.Map<Review>(reviewCreate);

                reviewMap.Recipe = _recipeRepository.GetRecipe(recipeId);
                reviewMap.Reviewer = _reviewerRepository.GetReviewer(reviewerId);



                if (!_reviewRepository.CreateReview(reviewMap))
                {
                    ModelState.AddModelError("", "Something went wrong while saving");
                    return StatusCode(500, ModelState);
                }

                return Ok("Successfully created");
            }
            catch
            {
                return StatusCode(500);
            }
        }


        [HttpPut("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview(int reviewId, [FromBody] ReviewDTO updatedReview)
        {
            try
            {
                if (updatedReview == null)
                    return BadRequest(ModelState);

                if (reviewId != updatedReview.Id)
                    return BadRequest(ModelState);

                if (!_reviewerRepository.ReviewerExists(reviewId))
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest();

                var reviewMap = _mapper.Map<Review>(updatedReview);

                if (!_reviewRepository.UpdateReview(reviewMap))
                {
                    ModelState.AddModelError("", "Something errored updating review");
                    return StatusCode(500, ModelState);
                }
                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReview(int reviewId)
        {
            try
            {
                if (!_reviewRepository.ReviewExists(reviewId))
                {
                    return NotFound();
                }

                var reviewToDelete = _reviewRepository.GetReview(reviewId);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!_reviewRepository.DeleteReview(reviewToDelete))
                {
                    ModelState.AddModelError("", "Something went wrong deleting review");
                }

                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
