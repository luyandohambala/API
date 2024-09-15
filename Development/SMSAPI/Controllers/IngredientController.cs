using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMSAPI.Dto.ClassRegisterDto;
using SMSAPI.Dto.IngredientDto;
using SMSAPI.Interfaces;
using SMSAPI.Models;
using SMSAPI.Repository;

namespace SMSAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class IngredientController : Controller
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;

        public IngredientController(IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }



        //get regular collectiion of ingredients
        [HttpGet]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Ingredient>))]
        public IActionResult GetIngredients()
        {
            var ingredients = _mapper.Map<List<IngredientReadDto>>(_ingredientRepository.GetIngredients());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(ingredients);
        }

        [HttpGet("Ingredient/Id/{ingredientId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(Ingredient))]
        public IActionResult GetIngredient(Guid ingredientId)
        {
            if (!_ingredientRepository.IngredientExists(ingredientId)) return NotFound();

            var ingredient = _mapper.Map<IngredientReadDto>(_ingredientRepository.GetIngredient(ingredientId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(ingredient);
        }


        /// <summary>
        /// Post request
        /// </summary>
        /// <param name="ingredientCreate"></param>
        /// <returns></returns> 
        //Post ingredient
        [HttpPost]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateIngredient([FromBody] IngredientCreateDto ingredientCreate)
        {
            if (ingredientCreate == null) return BadRequest(ModelState);

            if (_ingredientRepository.IngredientExists(ingredientCreate.IngredientId))
            {
                ModelState.AddModelError("", "Ingredient already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var ingredientMap = _mapper.Map<Ingredient>(ingredientCreate);

            if (!_ingredientRepository.CreateIngredient(ingredientMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }


        /// <summary>
        /// Put request
        /// </summary>
        /// <param name="ingredientId"></param>
        /// <param name="ingredientUpdate"></param>
        /// <returns></returns>
        //Put ingredient
        [HttpPut("Update/{ingredientId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateIngredient(Guid ingredientId, [FromBody] IngredientUpdateDto ingredientUpdate)
        {
            if (ingredientUpdate == null) return BadRequest(ModelState);

            if (ingredientId != ingredientUpdate.IngredientId)
            {
                ModelState.AddModelError("IdMissMatch", "Update Id and Body Id must match.");
                return BadRequest(ModelState);
            }

            if (!_ingredientRepository.IngredientExists(ingredientId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var ingredientMap = _mapper.Map<Ingredient>(ingredientUpdate);

            if (!_ingredientRepository.UpdateIngredient(ingredientMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        //calculate ingredient after meal preparation logic
        [HttpPut("Calculate/{ingredientId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult CalculateIngredient(Guid ingredientId, [FromBody] IngredientCalculateUpdateDto ingredientUpdate)
        {
            if (ingredientUpdate == null) return BadRequest(ModelState);

            if (ingredientId != ingredientUpdate.IngredientId)
            {
                ModelState.AddModelError("IdMissMatch", "Update Id and Body Id must match.");
                return BadRequest(ModelState);
            }

            if (!_ingredientRepository.IngredientExists(ingredientId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var ingredientMap = _mapper.Map<Ingredient>(ingredientUpdate);

            var ingredientDetails = _ingredientRepository.GetIngredient(ingredientMap.IngredientId);

            ingredientMap.IngredientName = ingredientDetails.IngredientName;
            ingredientMap.IngredientType = ingredientDetails.IngredientType;
            ingredientMap.IngredientMeasureType = ingredientDetails.IngredientMeasureType;

            if (!_ingredientRepository.UpdateIngredient(ingredientMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        /// <summary>
        /// Delete request
        /// </summary>
        /// <param name="ingredientId"></param>
        /// <returns></returns>
        //delete ingredient
        [HttpDelete("{ingredientId}")]
        //[ServiceFilter(typeof(AuthFilter))]   
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteIngredient(Guid ingredientId)
        {
            if (!_ingredientRepository.IngredientExists(ingredientId)) return NotFound();

            var classRegisterToDelete = _ingredientRepository.GetIngredient(ingredientId);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_ingredientRepository.DeleteIngredient(classRegisterToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting this Ingredient");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
