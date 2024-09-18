using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMSAPI.Dto.ClassRegisterDto;
using SMSAPI.Dto.MealPreparedDto;
using SMSAPI.Interfaces;
using SMSAPI.Models;
using SMSAPI.Repository;

namespace SMSAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class MealPreparedController : Controller
    {
        private readonly IMealPreparedRepository _mealPreparedRepository;
        private readonly IMapper _mapper;

        public MealPreparedController(IMealPreparedRepository mealPreparedRepository, IMapper mapper)
        {
            _mealPreparedRepository = mealPreparedRepository;
            _mapper = mapper;
        }



        /// <summary>
        /// http get requests
        /// </summary>
        /// <returns></returns>
        //get regular collection of prepared meals
        [HttpGet]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MealPrepared>))]
        public IActionResult GetMealsPrepared()
        {
            var mealsPrepared = _mapper.Map<List<MealPreparedReadDto>>(_mealPreparedRepository.GetMealsPrepared());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(mealsPrepared);
        }

        //get prepared meal by id
        [HttpGet("MealPrepared/Id/{mealPreparedId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(MealPrepared))]
        public IActionResult GetMealPrepared(Guid mealPreparedId)
        {
            if (!_mealPreparedRepository.MealPreparedExists(mealPreparedId)) return NotFound();

            var mealPrepared = _mapper.Map<MealPreparedReadDto>(_mealPreparedRepository.GetMealPrepared(mealPreparedId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(mealPrepared);
        }



        /// <summary>
        /// Post request
        /// </summary>
        /// <param name="mealPreparedCreate"></param>
        /// <returns></returns> 
        //Post mealPrepared
        [HttpPost]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateMealPrepared([FromBody] MealPreparedCreateDto mealPreparedCreate)
        {
            if (mealPreparedCreate == null) return BadRequest(ModelState);

            if (_mealPreparedRepository.MealPreparedExists(mealPreparedCreate.MealPreparedId))
            {
                ModelState.AddModelError("", "Pupil already exists on this register.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var mealPreparedMap = _mapper.Map<MealPrepared>(mealPreparedCreate);

            if (!_mealPreparedRepository.CreateMealPrepared(mealPreparedMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }


        /// <summary>
        /// Put request
        /// </summary>
        /// <param name="mealPreparedId"></param>
        /// <param name="mealPreparedUpdate"></param>
        /// <returns></returns>
        //Put mealPrepared
        [HttpPut("{mealPreparedId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateMealPrepared(Guid mealPreparedId, [FromBody] MealPreparedUpdateDto mealPreparedUpdate)
        {
            if (mealPreparedUpdate == null) return BadRequest(ModelState);

            if (mealPreparedId != mealPreparedUpdate.MealPreparedId)
            {
                ModelState.AddModelError("IdMissMatch", "Update Id and Body Id must match.");
                return BadRequest(ModelState);
            }

            if (!_mealPreparedRepository.MealPreparedExists(mealPreparedId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var mealPreparedMap = _mapper.Map<MealPrepared>(mealPreparedUpdate);

            if (!_mealPreparedRepository.UpdateMealPrepared(mealPreparedMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        /// <summary>
        /// Delete request
        /// </summary>
        /// <param name="mealPreparedId"></param>
        /// <returns></returns>
        //delete mealPrepared
        [HttpDelete("{mealPreparedId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteMealPrepared(Guid mealPreparedId)
        {
            if (!_mealPreparedRepository.MealPreparedExists(mealPreparedId)) return NotFound();

            var mealPreparedToDelete = _mealPreparedRepository.GetMealPrepared(mealPreparedId);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_mealPreparedRepository.DeleteMealPrepared(mealPreparedToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting the MealPrepared");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
