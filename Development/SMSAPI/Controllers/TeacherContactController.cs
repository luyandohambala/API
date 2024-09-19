using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMSAPI.Dto.TeacherContactDto;
using SMSAPI.Interfaces;
using SMSAPI.Models;

namespace SMSAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class TeacherContactController : Controller
    {
        private readonly ITeacherContactRepository _teacherContactRepository;
        private readonly IMapper _mapper;

        public TeacherContactController(ITeacherContactRepository teacherContactRepository, IMapper mapper)
        {
            _teacherContactRepository = teacherContactRepository;
            _mapper = mapper;
        }



        /// <summary>
        /// http request
        /// </summary>
        /// <returns></returns>
        //Get regular collection of TeacherContacts
        [HttpGet]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TeacherContact>))]
        public IActionResult GetTeacherContacts()
        {
            var TeacherContacts = _mapper.Map<List<TeacherContactReadDto>>(_teacherContactRepository.GetTeacherContacts());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(TeacherContacts);
        }

        //Get teacherid by teacherContactId
        [HttpGet("Teacher/{teacherContactId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TeacherContact>))]
        [ProducesResponseType(400)]
        public IActionResult GetGuardianIdByteacherContactId(Guid teacherContactId)
        {
            if (!_teacherContactRepository.TeacherContactExists(teacherContactId)) return NotFound();

            var teacherId = _teacherContactRepository.GetTeacherIdByTeacherContactId(teacherContactId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(teacherId);
        }

        //Get TeacherContact
        [HttpGet("TeacherContact/Id/{teacherContactId}")]
        //[ServiceFilter(typeof(AuthFilter))]   
        [ProducesResponseType(200, Type = typeof(Guid))]
        [ProducesResponseType(400)]
        public IActionResult GetteacherContactId(Guid teacherContactId)
        {
            if (!_teacherContactRepository.TeacherContactExists(teacherContactId)) return NotFound();

            var teacherContact = _teacherContactRepository.GetTeacherContact(teacherContactId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(teacherContact);
        }



        /// <summary>
        /// Post request
        /// </summary>
        /// <param name="TeacherContactCreate"></param>
        /// <returns></returns>
        //Post teachercontact 
        [HttpPost]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTeacherContact([FromBody] TeacherContactCreateDto TeacherContactCreate)
        {
            if (TeacherContactCreate == null) return BadRequest(ModelState);

            if (_teacherContactRepository.TeacherContactExists(TeacherContactCreate.TeacherContactId))
            {
                ModelState.AddModelError("", "Teacher contact already exists.");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var teacherContactMap = _mapper.Map<TeacherContact>(TeacherContactCreate);

            if (!_teacherContactRepository.CreateTeacherContact(teacherContactMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }


        /// <summary>
        /// Put request
        /// </summary>
        /// <param name="teacherContactId"></param>
        /// <param name="TeacherContactUpdate"></param>
        /// <returns></returns>
        //Put teachercontact
        [HttpPut("{teacherContactId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTeacherContact(Guid teacherContactId, [FromBody] TeacherContactUpdateDto TeacherContactUpdate)
        {
            if (TeacherContactUpdate == null) return BadRequest(ModelState);

            if (teacherContactId != TeacherContactUpdate.TeacherContactId)
            {
                ModelState.AddModelError("IdMissMatch", "Update Id and Body Id must match.");
                return BadRequest(ModelState);
            }

            if (!_teacherContactRepository.TeacherContactExists(teacherContactId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var teacherContactMap = _mapper.Map<TeacherContact>(TeacherContactUpdate);

            teacherContactMap.TeacherContactTeacherId = _teacherContactRepository.GetTeacherIdByTeacherContactId(teacherContactId);

            if (!_teacherContactRepository.UpdateTeacherContact(teacherContactMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        /// <summary>
        /// Delete request
        /// </summary>
        /// <param name="teacherContactId"></param>
        /// <returns></returns>
        //delete teachercontact
        [HttpDelete("{teacherContactId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTeacherContact(Guid teacherContactId)
        {
            if (!_teacherContactRepository.TeacherContactExists(teacherContactId)) return NotFound();

            var teacherContactToDelete = _teacherContactRepository.GetTeacherContact(teacherContactId);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_teacherContactRepository.DeleteTeacherContact(teacherContactToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting the TeacherContact");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
