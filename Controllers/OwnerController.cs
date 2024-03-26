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
    public class OwnerController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;
        public OwnerController(IOwnerRepository ownerRepository, IRecipeRepository recipeRepository,
            IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        public IActionResult GetOwners()
        {
            try
            {
                var owners = _mapper.Map<List<OwnerDTO>>(_ownerRepository.GetOwners());

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(owners);
            }

            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{ownerId}")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]
        public IActionResult GetOwner(int ownerId)
        {
            try
            {
                if (!_ownerRepository.OwnerExists(ownerId))
                    return NotFound();

                var owner = _mapper.Map<OwnerDTO>(_ownerRepository.GetOwner(ownerId));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(owner);
            }

            catch
            {
                return StatusCode(500);
            }
        }

         [HttpGet("{recipeId}/owner")]
         [ProducesResponseType(200, Type = typeof(Owner))]
         [ProducesResponseType(400)]
         public IActionResult GetOwnerByRecipe(int recipeId)
         {
            try
            {
                if (!_recipeRepository.RecipeExists(recipeId))
                {
                    return NotFound();
                }

                var owner = _mapper.Map<OwnerDTO>(
                 _ownerRepository.GetOwnerByRecipe(recipeId));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(owner);
            }

            catch
            {
                return StatusCode(500);
            }
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOwner([FromBody] OwnerDTO ownerCreate)
        {
            try
            {
                if (ownerCreate == null)
                    return BadRequest(ModelState);

                var owners = _ownerRepository.GetOwners()
                    .Where(c => c.Name.Trim().ToUpper() == ownerCreate.Name.TrimEnd().ToUpper())
                    .FirstOrDefault();

                if (owners != null)
                {
                    ModelState.AddModelError("", "Owner already exists");
                    return StatusCode(422, ModelState);

                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var ownerMap = _mapper.Map<Owner>(ownerCreate);

                if (!_ownerRepository.CreateOwner(ownerMap))
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



        [HttpPut("{ownerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOwner(int ownerId, [FromBody] OwnerDTO updatedOwner)
        {
            try
            {
                if (updatedOwner == null)
                    return BadRequest(ModelState);

                if (ownerId != updatedOwner.Id)
                    return BadRequest(ModelState);

                if (!_ownerRepository.OwnerExists(ownerId))
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest();

                var ownerMap = _mapper.Map<Owner>(updatedOwner);

                if (!_ownerRepository.UpdateOwner(ownerMap))
                {
                    ModelState.AddModelError("", "Something errored updating owner");
                    return StatusCode(500, ModelState);
                }
                return NoContent();
            }

            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{ownerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOwner(int ownerId)
        {
            try
            {
                if (!_ownerRepository.OwnerExists(ownerId))
                {
                    return NotFound();
                }

                var ownerToDelete = _ownerRepository.GetOwner(ownerId);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!_ownerRepository.DeleteOwner(ownerToDelete))
                {
                    ModelState.AddModelError("", "Something went wrong deleting owner");
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
