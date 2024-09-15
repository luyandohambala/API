using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMSAPI.Dto.ReportCardDto;
using SMSAPI.Dto.ReportCardSubjectDto;
using SMSAPI.Interfaces;
using SMSAPI.Models;
using SMSAPI.Repository;

namespace SMSAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class ReportCardController : Controller
    {
        private readonly IReportCardRepository _reportCardRepository;
        private readonly IMapper _mapper;

        public ReportCardController(IReportCardRepository reportCardRepository, IMapper mapper)
        {
            _reportCardRepository = reportCardRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// http get requests
        /// </summary>
        /// <returns></returns>
        //get regular collection of reportcards 
        [HttpGet]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReportCard>))]
        public IActionResult GetReportCards()
        {
            var reportCards = _mapper.Map<List<ReportCardReadDto>>(_reportCardRepository.GetReportCards());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(reportCards);
        }

        //get reportcard with subjects
        [HttpGet("ReportCardSubjects/{reportCardId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReportCardSubjects>))]
        [ProducesResponseType(400)]
        public IActionResult GetSubjectsByReportCardId(Guid reportCardId)
        {
            if (!_reportCardRepository.ReportCardExists(reportCardId)) return NotFound();

            var reportCardSubject = _mapper.Map<List<ReportCardSubjectReadDto>>(_reportCardRepository.GetReportCardSubjectsByReportCardId(reportCardId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(reportCardSubject);
        }

        //get reportcard with reportcardid
        [HttpGet("WithReportCardSubjects/Id/{reportCardId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(ReportCard))]
        [ProducesResponseType(400)]
        public IActionResult GetReportCard(Guid reportCardId)
        {
            if (!_reportCardRepository.ReportCardExists(reportCardId)) return NotFound();

            var reportCard = _mapper.Map<ReportCardWithReportCardSubjectsDto>(_reportCardRepository.GetReportCard(reportCardId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(reportCard);
        }

        //get pupilid by reportcard id
        [HttpGet("PupilId/{reportCardId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(Guid))]
        [ProducesResponseType(400)]
        public IActionResult GetPupilIdByReportCardId(Guid reportCardId)
        {
            if (!_reportCardRepository.ReportCardExists(reportCardId)) return NotFound();

            var pupilId = _reportCardRepository.GetPupilIdByReportCardId(reportCardId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(pupilId);
        }
        
        //get classid by reportcardid
        [HttpGet("ClassRoomId/{reportCardId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(Guid))]
        [ProducesResponseType(400)]
        public IActionResult GetClassRoomIdByReportCardId(Guid reportCardId)
        {
            if (!_reportCardRepository.ReportCardExists(reportCardId)) return NotFound();

            var classRoomId = _reportCardRepository.GetClassRoomIdByReportCardId(reportCardId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(classRoomId);
        }



        /// <summary>
        /// http post request
        /// </summary>
        /// <param name="reportCardCreate"></param>
        /// <returns></returns>
        //post reportcard
        [HttpPost]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReportCard([FromBody] ReportCardCreateDto reportCardCreate)
        {
            if (reportCardCreate == null) return BadRequest(ModelState);

            if (_reportCardRepository.ReportCardExists(reportCardCreate.ReportCardId))
            {
                ModelState.AddModelError("", "ReportCard already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var reportCardMap = _mapper.Map<ReportCard>(reportCardCreate);

            if (!_reportCardRepository.CreateReportCard(reportCardMap))  
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }



        /// <summary>
        /// http put request 
        /// </summary>
        /// <param name="reportCardId"></param>
        /// <param name="reportCardUpdate"></param>
        /// <returns></returns>
        //update reportcard
        [HttpPut("{reportCardId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReportCard(Guid reportCardId, [FromBody] ReportCardUpdateDto reportCardUpdate)
        {
            if (reportCardUpdate == null) return BadRequest(ModelState);

            if (reportCardId != reportCardUpdate.ReportCardId)
            {
                ModelState.AddModelError("IdMissMatch", "Update Id and Body Id must match.");
                return BadRequest(ModelState);
            }

            if (!_reportCardRepository.ReportCardExists(reportCardId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var reportCardMap = _mapper.Map<ReportCard>(reportCardUpdate);

            reportCardMap.ReportCardClassId = _reportCardRepository.GetClassRoomIdByReportCardId(reportCardId);

            reportCardMap.ReportCardPupilId = _reportCardRepository.GetPupilIdByReportCardId(reportCardId);

            if (!_reportCardRepository.UpdateReportCard(reportCardMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }



        //delete reportCard
        [HttpDelete("{reportCardId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteClassRoom(Guid reportCardId)
        {
            if (!_reportCardRepository.ReportCardExists(reportCardId)) return NotFound();

            var reportCardToDelete = _reportCardRepository.GetReportCard(reportCardId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_reportCardRepository.DeleteReportCard(reportCardToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting the classroom");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
