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
    public class ReviewerController : Controller
    {
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;

        public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
        {
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
        public IActionResult GetReviewers()
        {
            try
            {
                var reviewers = _mapper.Map<List<ReviewerDTO>>(_reviewerRepository.GetReviewers());

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(reviewers);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewer(int reviewerId)
        {
            try
            {
                if (!_reviewerRepository.ReviewerExists(reviewerId))
                    return NotFound();

                var reviewer = _mapper.Map<ReviewerDTO>(_reviewerRepository.GetReviewer(reviewerId));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(reviewer);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{reviewerId}/reviews")]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            try
            {
                if (!_reviewerRepository.ReviewerExists(reviewerId))
                    return NotFound();

                var reviews = _mapper.Map<List<ReviewDTO>>(
                    _reviewerRepository.GetReviewsByReviewer(reviewerId));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

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
        public IActionResult CreateReviewer([FromBody] ReviewerDTO reviewerCreate)
        {
            try
            {
                if (reviewerCreate == null)
                    return BadRequest(ModelState);

                var reviewer = _reviewerRepository.GetReviewers()
                    .Where(c => c.Name.Trim().ToUpper() == reviewerCreate.Name.TrimEnd().ToUpper())
                    .FirstOrDefault();

                if (reviewer != null)
                {
                    ModelState.AddModelError("", "Reviewer already exists");
                    return StatusCode(422, ModelState);

                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var reviewerMap = _mapper.Map<Reviewer>(reviewerCreate);

                if (!_reviewerRepository.CreateReviewer(reviewerMap))
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


        [HttpPut("{reviewerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReviewer(int reviewerId, [FromBody] ReviewerDTO updatedReviewer)
        {
            try
            {
                if (updatedReviewer == null)
                    return BadRequest(ModelState);

                if (reviewerId != updatedReviewer.Id)
                    return BadRequest(ModelState);

                if (!_reviewerRepository.ReviewerExists(reviewerId))
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest();

                var reviewerMap = _mapper.Map<Reviewer>(updatedReviewer);

                if (!_reviewerRepository.UpdateReviewer(reviewerMap))
                {
                    ModelState.AddModelError("", "Something errored updating reviewer");
                    return StatusCode(500, ModelState);
                }
                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{reviewerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReviewer(int reviewerId)
        {
            try
            {
                if (!_reviewerRepository.ReviewerExists(reviewerId))
                {
                    return NotFound();
                }

                var reviewerToDelete = _reviewerRepository.GetReviewer(reviewerId);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!_reviewerRepository.DeleteReviewer(reviewerToDelete))
                {
                    ModelState.AddModelError("", "Something went wrong deleting reviewer");
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
