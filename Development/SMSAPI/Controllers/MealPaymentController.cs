using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMSAPI.Dto.MealPaymentDto;
using SMSAPI.Interfaces;
using SMSAPI.Models;
using SMSAPI.Repository;

namespace SMSAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class MealPaymentController : Controller
    {
        private readonly IMealPaymentRepository _mealPaymentRepository;
        private readonly IMapper _mapper;

        public MealPaymentController(IMealPaymentRepository mealPaymentRepository, IMapper mapper)
        {
            _mealPaymentRepository = mealPaymentRepository;
            _mapper = mapper;
        }



        //get regular collection of mealpayment




        /// <summary>
        /// Post request
        /// </summary>
        /// <param name="MealPaymentCreate"></param>
        /// <returns></returns> 
        //Post MealPayment
        [HttpPost]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateMealPayment([FromBody] MealPaymentCreateDto MealPaymentCreate)
        {
            if (MealPaymentCreate == null) return BadRequest(ModelState);

            if (_mealPaymentRepository.MealPaymentExists(MealPaymentCreate.MealPaymentId))
            {
                ModelState.AddModelError("", "MealPaymentStatus already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var mealPaymentMap = _mapper.Map<MealPayment>(MealPaymentCreate);

            if (!_mealPaymentRepository.CreateMealPayment(mealPaymentMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }


        /// <summary>
        /// Put request
        /// </summary>
        /// <param name="mealPaymentId"></param>
        /// <param name="MealPaymentUpdate"></param>
        /// <returns></returns>
        //Put MealPayment
        [HttpPut("Update/{mealPaymentId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateMealPayment(Guid mealPaymentId, [FromBody] MealPaymentUpdateDto MealPaymentUpdate)
        {
            if (MealPaymentUpdate == null) return BadRequest(ModelState);

            if (mealPaymentId != MealPaymentUpdate.MealPaymentId)
            {
                ModelState.AddModelError("IdMissMatch", "Update Id and Body Id must match.");
                return BadRequest(ModelState);
            }

            if (!_mealPaymentRepository.MealPaymentExists(mealPaymentId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var mealPaymentMap = _mapper.Map<MealPayment>(MealPaymentUpdate);

            mealPaymentMap.MealPaymentPupilId = _mealPaymentRepository.GetMealPayment(mealPaymentId).MealPaymentPupilId;

            if (!_mealPaymentRepository.UpdateMealPayment(mealPaymentMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        
        /// <summary>
        /// Put request
        /// </summary>
        /// <param name="mealPaymentId"></param>
        /// <param name="MealPaymentUpdate"></param>
        /// <returns></returns>
        //Put MealPayment
        [HttpPut("UpdateStatus/{mealPaymentId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateStatusMealPayment(Guid mealPaymentId, [FromBody] MealPaymentUpdateDto MealPaymentUpdate)
        {
            if (MealPaymentUpdate == null) return BadRequest(ModelState);

            if (mealPaymentId != MealPaymentUpdate.MealPaymentId)
            {
                ModelState.AddModelError("IdMissMatch", "Update Id and Body Id must match.");
                return BadRequest(ModelState);
            }

            if (!_mealPaymentRepository.MealPaymentExists(mealPaymentId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var mealPaymentMap = _mapper.Map<MealPayment>(MealPaymentUpdate);

            var MealPaymentDetails = _mealPaymentRepository.GetMealPayment(mealPaymentMap.MealPaymentId);

            mealPaymentMap.MealPaymentPupilName = MealPaymentDetails.MealPaymentPupilName;
            mealPaymentMap.MealPaymentPeriod = MealPaymentDetails.MealPaymentPeriod;
            mealPaymentMap.MealPaymentDate = MealPaymentDetails.MealPaymentDate;
            mealPaymentMap.MealPaymentAmount = MealPaymentDetails.MealPaymentAmount;
            mealPaymentMap.MealPaymentPupilId = MealPaymentDetails.MealPaymentPupilId;
            

            if (!_mealPaymentRepository.UpdateMealPayment(mealPaymentMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        /// <summary>
        /// Delete request
        /// </summary>
        /// <param name="mealPaymentId"></param>
        /// <returns></returns>
        //delete MealPayment
        [HttpDelete("{mealPaymentId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteMealPayment(Guid mealPaymentId)
        {
            if (!_mealPaymentRepository.MealPaymentExists(mealPaymentId)) return NotFound();

            var mealPaymentToDelete = _mealPaymentRepository.GetMealPayment(mealPaymentId);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_mealPaymentRepository.DeleteMealPayment(mealPaymentToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting the MealPaymentStatus");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
