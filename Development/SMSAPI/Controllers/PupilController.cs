using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMSAPI.Dto.PupilDto;
using SMSAPI.Interfaces;
using SMSAPI.Models;
using SMSAPI.Repository;

namespace SMSAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class PupilController : Controller
    {
        private readonly IPupilRepository _pupilRepository;
        private readonly IReportCardRepository _reportCardRepository;
        private readonly IMapper _mapper;

        public PupilController(IPupilRepository pupilRepository, IReportCardRepository reportCardRepository, IMapper mapper)
        {
            _pupilRepository = pupilRepository;
            _reportCardRepository = reportCardRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// http get requests
        /// </summary>
        /// <returns></returns>
        //Get regular collection of pupils
        [HttpGet]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pupil>))]
        public IActionResult GetPupils()
        {
            var pupils = _mapper.Map<List<Pupil>>(_pupilRepository.GetPupils());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(pupils);
        }

        //get pupil with reportcard 
        [HttpGet("WithReportCard/Id/{pupilId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(Pupil))]
        [ProducesResponseType(400)]
        public IActionResult GetPupilWithReportCard(Guid pupilId)
        {
            if (!_pupilRepository.PupilExists(pupilId)) return NotFound();

            var pupil = _mapper.Map<PupilWithReportCardsDto>(_pupilRepository.GetPupil(pupilId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(pupil);
        }

        //get guardian id
        [HttpGet("GuardianId/{pupilId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(Guid))]
        [ProducesResponseType(400)]
        public IActionResult GetGuardianIdByPupilId(Guid pupilId)
        {
            if (!_pupilRepository.PupilExists(pupilId)) return NotFound();

            var guardianId = _pupilRepository.GetGuardianIdByPupilId(pupilId);
            
            if(!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(guardianId);
        }
        
        //get grade id
        [HttpGet("GradeId/{pupilId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(Guid))]
        [ProducesResponseType(400)]
        public IActionResult GetGradeIdByPupilId(Guid pupilId)
        {
            if (!_pupilRepository.PupilExists(pupilId)) return NotFound();

            var gradeId = _pupilRepository.GetGradeIdByPupilId(pupilId);
            
            if(!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(gradeId);
        }
        
        //get class id
        [HttpGet("ClassId/{pupilId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(Guid))]
        [ProducesResponseType(400)]
        public IActionResult GetClassIdByPupilId(Guid pupilId)
        {
            if (!_pupilRepository.PupilExists(pupilId)) return NotFound();

            var classId = _pupilRepository.GetClassIdByPupilId(pupilId);
            
            if(!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(classId);
        }


        /// <summary>
        /// http post request
        /// </summary>
        /// <param name="pupilCreate"></param>
        /// <returns></returns>
        //post pupil
        [HttpPost]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePupil([FromBody] PupilCreateDto pupilCreate)
        {
            if (pupilCreate == null) return BadRequest(ModelState);

            if (_pupilRepository.PupilExists(pupilCreate.PupilId))
            {
                ModelState.AddModelError("", "Pupil already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var pupilMap = _mapper.Map<Pupil>(pupilCreate);

            if (!_pupilRepository.CreatePupil(pupilMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }


        /// <summary>
        /// http put request
        /// </summary>
        /// <param name="pupilId"></param>
        /// <param name="pupilUpdate"></param>
        /// <returns></returns>
        //update pupil
        [HttpPut("{pupilId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePupil(Guid pupilId, [FromBody] PupilUpdateDto pupilUpdate)
        {
            if (pupilUpdate == null) return BadRequest(ModelState);

            if (pupilId != pupilUpdate.PupilId)
            {
                ModelState.AddModelError("IdMissMatch", "Update Id and Body Id must match.");
                return BadRequest(ModelState);
            }

            if (!_pupilRepository.PupilExists(pupilId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var pupilMap = _mapper.Map<Pupil>(pupilUpdate);

            if (!_pupilRepository.UpdatePupil(pupilMap))
            {
                ModelState.AddModelError("", "Something wen wrong while updating");
                return StatusCode(500, ModelState); 
            }

            return NoContent();
        }


        /// <summary>
        /// http remove request
        /// </summary>
        /// <param name="pupilId"></param>
        /// <returns></returns>
        //delete pupil
        [HttpDelete("{pupilId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeletePupil(Guid pupilId)
        {
            if (!_pupilRepository.PupilExists(pupilId)) return NotFound();

            var reportCardsToDelete = _pupilRepository.GetReportCardByPupilId(pupilId);

            var pupilToDelete = _pupilRepository.GetPupil(pupilId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (reportCardsToDelete.Count != 0)
            {
                if (!_reportCardRepository.DeleteReportCards([.. reportCardsToDelete]))
                {
                    ModelState.AddModelError("", "Something went wrong while deleting the ReportCards belonging to this Pupil");
                    return StatusCode(500, ModelState);
                }
            }

            if (!_pupilRepository.DeletePupil(pupilToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting this Pupil");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
