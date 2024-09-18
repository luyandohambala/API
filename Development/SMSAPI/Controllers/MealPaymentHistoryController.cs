using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMSAPI.Dto.MealPaymentHistoryDto;
using SMSAPI.Interfaces;
using SMSAPI.Models;

namespace SMSAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class MealPaymentHistoryController : Controller
    {
        private readonly IMealPaymentHistoryRepository _mealPaymentHistoryRepository;
        private readonly IPupilRepository _pupilRepository;
        private readonly IMapper _mapper;

        public MealPaymentHistoryController(IMealPaymentHistoryRepository mealPaymentHistoryRepository, IPupilRepository pupilRepository, IMapper mapper)
        {
            _mealPaymentHistoryRepository = mealPaymentHistoryRepository;
            _pupilRepository = pupilRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// http get requests
        /// </summary>
        /// <returns></returns>
        //get regular collection of mealPaymentHistories
        [HttpGet]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MealPaymentHistory>))]
        public IActionResult GetMealPaymentHistory()
        {
            var mealPaymentHistories = _mapper.Map<List<MealPaymentHistoryReadDto>>(_mealPaymentHistoryRepository.GetMealPaymentHistories());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(mealPaymentHistories);
        }
         
        //get mealpaymenthistory by id
        [HttpGet("MealPaymentHistory/Id/{mealPaymentHistoryId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(MealPaymentHistory))]
        [ProducesResponseType(400)]
        public IActionResult GetMealPaymentHistory(Guid mealPaymentHistoryId)
        {
            if (!_mealPaymentHistoryRepository.MealPaymentHistoryExists(mealPaymentHistoryId)) return NotFound();

            var mealPaymentHistory = _mapper.Map<MealPaymentHistoryReadDto>(_mealPaymentHistoryRepository.GetMealPaymentHistory(mealPaymentHistoryId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(mealPaymentHistory);
        }

        //get mealPaymentHistory by pupilID
        [HttpGet("PupilId/{mealPaymentHistoryId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MealPaymentHistory>))]
        [ProducesResponseType(400)]
        public IActionResult GetMealPaymentHistoryByPupilId(Guid pupilId)
        {
            if (!_pupilRepository.PupilExists(pupilId)) return NotFound();

            var mealPaymentHistories = _mapper.Map<List<MealPaymentHistoryReadDto>>(_mealPaymentHistoryRepository.GetMealPaymentHistoriesByPupilId(pupilId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(mealPaymentHistories);
        }




        /// <summary>
        /// Post request
        /// </summary>
        /// <param name="mealPaymentHistoryCreate"></param>
        /// <returns></returns> 
        //Post mealPaymentHistory
        [HttpPost]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatemealPaymentHistory([FromBody] MealPaymentHistoryCreateDto mealPaymentHistoryCreate)
        {
            if (mealPaymentHistoryCreate == null) return BadRequest(ModelState);

            if (_mealPaymentHistoryRepository.MealPaymentHistoryExists(mealPaymentHistoryCreate.MealPaymentId))
            {
                ModelState.AddModelError("", "MealPayment with this Id already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var mealPaymentHistoryMap = _mapper.Map<MealPaymentHistory>(mealPaymentHistoryCreate);

            if (!_mealPaymentHistoryRepository.CreateMealPaymentHistory(mealPaymentHistoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }


        /// <summary>
        /// Put request
        /// </summary>
        /// <param name="mealPaymentHistoryId"></param>
        /// <param name="mealPaymentHistoryUpdate"></param>
        /// <returns></returns>
        //Put mealPaymentHistory
        [HttpPut("{mealPaymentHistoryId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdatemealPaymentHistory(Guid mealPaymentHistoryId, [FromBody] MealPaymentHistoryUpdateDto mealPaymentHistoryUpdate)
        {
            if (mealPaymentHistoryUpdate == null) return BadRequest(ModelState);

            if (mealPaymentHistoryId != mealPaymentHistoryUpdate.MealPaymentId)
            {
                ModelState.AddModelError("IdMissMatch", "Update Id and Body Id must match.");
                return BadRequest(ModelState);
            }

            if (!_mealPaymentHistoryRepository.MealPaymentHistoryExists(mealPaymentHistoryId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var mealPaymentHistoryMap = _mapper.Map<MealPaymentHistory>(mealPaymentHistoryUpdate);

            var mealPaymentHistoryDetails = _mealPaymentHistoryRepository.GetMealPaymentHistory(mealPaymentHistoryMap.MealPaymentId);

            mealPaymentHistoryMap.MealPaymentPupilName = mealPaymentHistoryDetails.MealPaymentPupilName;
            mealPaymentHistoryMap.MealPaymentPupilId = mealPaymentHistoryDetails.MealPaymentPupilId;

            if (!_mealPaymentHistoryRepository.UpdateMealPaymentHistory(mealPaymentHistoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        /// <summary>
        /// Delete request
        /// </summary>
        /// <param name="mealPaymentHistoryId"></param>
        /// <returns></returns>
        //delete mealPaymentHistory
        [HttpDelete("{mealPaymentHistoryId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeletemealPaymentHistory(Guid mealPaymentHistoryId)
        {
            if (!_mealPaymentHistoryRepository.MealPaymentHistoryExists(mealPaymentHistoryId)) return NotFound();

            var mealPaymentHistoryToDelete = _mealPaymentHistoryRepository.GetMealPaymentHistory(mealPaymentHistoryId);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_mealPaymentHistoryRepository.DeleteMealPaymentHistory(mealPaymentHistoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting this MealPaymentHistory");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
