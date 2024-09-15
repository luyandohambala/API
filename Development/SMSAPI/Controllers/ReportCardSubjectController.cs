using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMSAPI.Dto.ClassRoomDto;
using SMSAPI.Dto.ReportCardSubjectDto;
using SMSAPI.Interfaces;
using SMSAPI.Models;
using SMSAPI.Repository;

namespace SMSAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class ReportCardSubjectController : Controller
    {
        private readonly IReportCardSubjectRepository _reportCardSubjectRepository;
        private readonly IMapper _mapper;

        public ReportCardSubjectController(IReportCardSubjectRepository reportCardSubjectRepository, IMapper mapper)
        {
            _reportCardSubjectRepository = reportCardSubjectRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// http get requests
        /// </summary>
        /// <returns></returns>
        //get regular collection of reportcardsubjects
        [HttpGet]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReportCardSubjects>))]
        public IActionResult GetReportCardSubject()
        {
            var reportCardSubjects = _mapper.Map<List<ReportCardSubjectReadDto>>(_reportCardSubjectRepository.GetReportCardSubjects());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(reportCardSubjects); 
        }

        //get reportcardsubjects beloing to a specific reportcard
        [HttpGet("ReportCardSubject/Id/{reportCardSubjectId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(ReportCardSubjects))]
        [ProducesResponseType(400)]
        public IActionResult GetReportCardSubject(Guid reportCardSubjectId)
        {
            if (!_reportCardSubjectRepository.ReportCardSubjectExists(reportCardSubjectId)) return NotFound();

            var reportCardSubject = _mapper.Map<ReportCardSubjectReadDto>(_reportCardSubjectRepository.GetReportCardSubject(reportCardSubjectId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(reportCardSubject);
        }

        //get reportcardid of reportcardsubject
        [HttpGet("ReportCardId/{reportCardSubjectId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReportCard>))]
        [ProducesResponseType(400)]
        public IActionResult GetReportCardIdByReportCardSubjectId(Guid reportCardSubjectId)
        {
            if (!_reportCardSubjectRepository.ReportCardSubjectExists(reportCardSubjectId)) return NotFound();

            var reportCardIds = _reportCardSubjectRepository.GetReportCardIdByReportCardSubjectId(reportCardSubjectId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(reportCardIds);
        }



        /// <summary>
        /// Post request
        /// </summary>
        /// <param name="reportCardSubjectCreate"></param>
        /// <returns></returns>
        //Post classroom
        [HttpPost]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatereportCardSubject([FromBody] ReportCardSubjectCreateDto reportCardSubjectCreate)
        {
            if (reportCardSubjectCreate == null) return BadRequest(ModelState);

            if (_reportCardSubjectRepository.ReportCardSubjectExists(reportCardSubjectCreate.ReportCardSubjectId))
            {
                ModelState.AddModelError("", "ReportCardSubject already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var reportCardSubjectMap = _mapper.Map<ReportCardSubjects>(reportCardSubjectCreate);

            if (!_reportCardSubjectRepository.CreateReportCardSubject(reportCardSubjectMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }




        /// <summary>
        /// Put request
        /// </summary>
        /// <param name="reportCardSubjectId"></param>
        /// <param name="reportCardSubjectUpdate"></param>
        /// <returns></returns>
        //Put classroom
        [HttpPut("{reportCardSubjectId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdatereportCardSubject(Guid reportCardSubjectId, [FromBody] ReportCardSubjectUpdateDto reportCardSubjectUpdate)
        {
            if (reportCardSubjectUpdate == null) return BadRequest(ModelState);

            if (reportCardSubjectId != reportCardSubjectUpdate.ReportCardSubjectId)
            {
                ModelState.AddModelError("IdMissMatch", "Update Id and Body Id must match.");
                return BadRequest(ModelState);
            }

            if (!_reportCardSubjectRepository.ReportCardSubjectExists(reportCardSubjectId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var reportCardSubjectMap = _mapper.Map<ReportCardSubjects>(reportCardSubjectUpdate);

            if (!_reportCardSubjectRepository.UpdateReportCardSubject(reportCardSubjectMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }




        /// <summary>
        /// Delete request
        /// </summary>
        /// <param name="reportCardSubjectId"></param>
        /// <returns></returns>
        //delete classroom
        [HttpDelete("{reportCardSubjectId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteClassRoom(Guid reportCardSubjectId) 
        { 
            if (!_reportCardSubjectRepository.ReportCardSubjectExists(reportCardSubjectId)) return NotFound();

            var reportCardSubjectToDelete = _reportCardSubjectRepository.GetReportCardSubject(reportCardSubjectId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_reportCardSubjectRepository.DeleteReportCardSubject(reportCardSubjectToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting the ReportCardSubject");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
