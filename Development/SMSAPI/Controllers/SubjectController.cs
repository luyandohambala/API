using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMSAPI.Dto.ClassRoomDto;
using SMSAPI.Dto.SubjectDto;
using SMSAPI.Interfaces;
using SMSAPI.Models;
using SMSAPI.Repository;

namespace SMSAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class SubjectController : Controller
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public SubjectController(ISubjectRepository subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// http get request
        /// </summary>
        /// <returns></returns>
        //get regular collection of subjects
        [HttpGet]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Subject>))]
        public IActionResult GetSubjects()
        {
            var subjects = _mapper.Map<List<SubjectReadDto>>(_subjectRepository.GetSubjects());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(subjects);    
        }

        //get classroooms belonging to a subject
        [HttpGet("ClassRooms/{subjectId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ClassRoom>))]
        [ProducesResponseType(400)]
        public IActionResult GetClassRoomsBySubjectId(Guid subjectId)
        {
            if (!_subjectRepository.SubjectExists(subjectId)) return NotFound();

            var classRooms = _mapper.Map<List<ClassRoomReadDto>>(_subjectRepository.GetClassRoomsBySubjectId(subjectId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(subjectId);
        }

        //get subject by subjectid
        [HttpGet("Subject/Id/{subjectId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(Subject))]
        [ProducesResponseType(400)]
        public IActionResult GetSubject(Guid subjectId)
        {
            if (!_subjectRepository.SubjectExists(subjectId)) return NotFound();

            var subject = _mapper.Map<SubjectReadDto>(_subjectRepository.GetSubject(subjectId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(subject);
        }



        /// <summary>
        /// Post request
        /// </summary>
        /// <param name="subjectCreate"></param>
        /// <returns></returns>
        //Post classroom
        [HttpPost]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateClassRoom([FromBody] SubjectCreateDto subjectCreate)
        {
            if (subjectCreate == null) return BadRequest(ModelState);

            if (_subjectRepository.SubjectExists(subjectCreate.SubjectId))
            {
                ModelState.AddModelError("", "subject already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var subjectMap = _mapper.Map<Subject>(subjectCreate);

            if (!_subjectRepository.CreateSubject(subjectMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }



        /// <summary>
        /// Put request
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="subjectUpdate"></param>
        /// <returns></returns>
        //Put classroom
        [HttpPut("{subjectId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateClassRoom(Guid subjectId, [FromBody] SubjectUpdateDto subjectUpdate)
        {
            if (subjectUpdate == null) return BadRequest(ModelState);

            if (subjectId != subjectUpdate.SubjectId)
            {
                ModelState.AddModelError("IdMissMatch", "Update Id and Body Id must match.");
                return BadRequest(ModelState);
            }

            if (!_subjectRepository.SubjectExists(subjectId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var subjectMap = _mapper.Map<Subject>(subjectUpdate);

            subjectMap.SubjectGradeId = _subjectRepository.GetClassRoomsBySubjectId(subjectId).ToList().Select(cr => cr.ClassGradeId).FirstOrDefault();

            if (!_subjectRepository.UpdateSubject(subjectMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }



        /// <summary>
        /// Delete request
        /// </summary>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        //delete classroom
        [HttpDelete("{subjectId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteClassRoom(Guid subjectId)
        {
            if (!_subjectRepository.SubjectExists(subjectId)) return NotFound();

            var subjectToDelete = _subjectRepository.GetSubject(subjectId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_subjectRepository.DeleteSubject(subjectToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting the subject");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
