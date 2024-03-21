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
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public RecipeController(IRecipeRepository recipeRepository, 
            IReviewRepository reviewRepository, IOwnerRepository ownerRepository,
            IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _reviewRepository = reviewRepository;
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Recipe>))]
        public IActionResult GetRecipes()
        {
            var recipes = _mapper.Map<List<RecipeDTO>>(_recipeRepository.GetRecipes());

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(recipes);
        }

        [HttpGet("{recipeId}")]
        [ProducesResponseType(200, Type = typeof(Recipe))]
        [ProducesResponseType (400)]
        public IActionResult GetRecipe(int recipeId)
        {
            if (!_recipeRepository.RecipeExists(recipeId))
                return NotFound();

            var recipe = _mapper.Map<RecipeDTO>(_recipeRepository.GetRecipe(recipeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(recipe);
        }

        [HttpGet("{recipeId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]

        public IActionResult GetRecipeRating(int recipeId)
        {
            if (!_recipeRepository.RecipeExists(recipeId))
                return NotFound();

            var rating = _recipeRepository.GetRecipeRating(recipeId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(rating);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRecipe([FromQuery] int ownerId, [FromQuery] int catId, [FromBody] RecipeDTO recipeCreate)
        {
            if (recipeCreate == null)
                return BadRequest(ModelState);

            var recipes = _recipeRepository.GetRecipes()
                .Where(c => c.Name.Trim().ToUpper() == recipeCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (recipes != null)
            {
                ModelState.AddModelError("", "Recipe already exists");
                return StatusCode(422, ModelState);

            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var recipeMap = _mapper.Map<Recipe>(recipeCreate);

            recipeMap.Owner = _ownerRepository.GetOwner(ownerId);

            if (!_recipeRepository.CreateRecipe( catId, recipeMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }

        [HttpPut("{recipeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateRecipe(int recipeId, [FromQuery] int ownerId, [FromQuery]
            int categoryId, [FromBody] RecipeDTO updatedRecipe)
        {
            if (updatedRecipe == null)
                return BadRequest(ModelState);

            if (recipeId != updatedRecipe.Id)
                return BadRequest(ModelState);

            if (!_recipeRepository.RecipeExists(recipeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var recipeMap = _mapper.Map<Recipe>(updatedRecipe);

            if (!_recipeRepository.UpdateRecipe(ownerId, categoryId, recipeMap))
            {
                ModelState.AddModelError("", "Something errored updating recipe");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{recipeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRecipe(int recipeId)
        {
            if (!_recipeRepository.RecipeExists(recipeId))
            {
                return NotFound();
            }

            var reviewsToDelete = _reviewRepository.GetReviewsOfRecipe(recipeId);
            var recipeToDelete = _recipeRepository.GetRecipe(recipeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.DeleteReviews(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong when deleting reviews");
            }

            if (!_recipeRepository.DeleteRecipe(recipeToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting recipe");
            }
            return NoContent();

        }
    }
}
