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
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository,
            IRecipeRepository recipeRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            try
            {
                var categories = _mapper.Map<List<CategoryDTO>>(_categoryRepository.GetCategories());

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(categories);
            }

            catch
            {
                return StatusCode(500);
            }
        }


        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int categoryId)
        {
            try
            {
                if (!_categoryRepository.CategoryExists(categoryId))
                    return NotFound();

                var category = _mapper.Map<CategoryDTO>(_categoryRepository.GetCategory(categoryId));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(category);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("recipe/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Recipe>))]
        [ProducesResponseType(400)]
        public IActionResult GetRecipeByCategoryId(int categoryId)
        {
            try
            {
                var recipes = _mapper.Map<List<RecipeDTO>>(
                    _categoryRepository.GetRecipeByCategory(categoryId));
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(recipes);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{recipeId}/category")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategoryByRecipe(int recipeId)
        {
            try
            {
                if (!_recipeRepository.RecipeExists(recipeId))
                {
                    return NotFound();
                }

                var category = _mapper.Map<CategoryDTO>(
                 _categoryRepository.GetCategoryByRecipe(recipeId));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(category);
            }
            catch
            {
                return StatusCode(500);
            }
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDTO categoryCreate)
        {
            try
            {
                if (categoryCreate == null)
                    return BadRequest(ModelState);

                var category = _categoryRepository.GetCategories()
                    .Where(c => c.Type.Trim().ToUpper() == categoryCreate.Type.TrimEnd().ToUpper())
                    .FirstOrDefault();

                if (category != null)
                {
                    ModelState.AddModelError("", "Category already exists");
                    return StatusCode(422, ModelState);

                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var categoryMap = _mapper.Map<Category>(categoryCreate);

                if (!_categoryRepository.CreateCategory(categoryMap))
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

        [HttpPut("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory (int categoryId, [FromBody]CategoryDTO updatedCategory)
        {
            try
            {
                if (updatedCategory == null)
                    return BadRequest(ModelState);

                if (categoryId != updatedCategory.Id)
                    return BadRequest(ModelState);

                if (!_categoryRepository.CategoryExists(categoryId))
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest();

                var categoryMap = _mapper.Map<Category>(updatedCategory);

                if (!_categoryRepository.UpdateCategory(categoryMap))
                {
                    ModelState.AddModelError("", "Something errored updating category");
                    return StatusCode(500, ModelState);
                }
                return NoContent();
            }

            catch 
            { 
                return StatusCode(500); 
            }
        }

        [HttpDelete("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int categoryId)
        {
            try
            {
                if (!_categoryRepository.CategoryExists(categoryId))
                {
                    return NotFound();
                }

                var categoryToDelete = _categoryRepository.GetCategory(categoryId);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!_categoryRepository.DeleteCategory(categoryToDelete))
                {
                    ModelState.AddModelError("", "Something went wrong deleting category");
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
