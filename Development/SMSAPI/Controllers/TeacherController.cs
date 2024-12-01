using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMSAPI.Dto.SubjectDto;
using SMSAPI.Dto.TeacherDto;
using SMSAPI.Interfaces;
using SMSAPI.Models;

namespace SMSAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class TeacherController : Controller
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly ITeacherAddressRepository _teacherAddressRepository;
        private readonly ITeacherContactRepository _teacherContactRepository;
        private readonly IMapper _mapper;

        public TeacherController(ITeacherRepository teacherRepository, ITeacherAddressRepository teacherAddressRepository, ITeacherContactRepository teacherContactRepository, IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _teacherAddressRepository = teacherAddressRepository;
            _teacherContactRepository = teacherContactRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// http get requests
        /// </summary>
        /// <returns></returns>
        //get regular collection of teachers
        [HttpGet]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Teacher>))]
        public IActionResult GetTeachers()
        {
            var teachers = _mapper.Map<List<TeacherReadDto>>(_teacherRepository.GetTeachers());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(teachers);
        }

        [HttpGet("Subjects/{teacherId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Subject>))]
        [ProducesResponseType(400)]
        public IActionResult GetSubjectsByTeacherId(Guid teacherId)
        {
            if (!_teacherRepository.TeacherExists(teacherId)) return NotFound();

            var subjects = _mapper.Map<List<SubjectReadDto>>(_teacherRepository.GetSubjectsByTeacherId(teacherId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(subjects);
        }

        [HttpGet("Teacher/Id/{teacherId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(Teacher))]
        [ProducesResponseType(400)]
        public IActionResult GetTeacher(Guid teacherId)
        {
            if (!_teacherRepository.TeacherExists(teacherId)) return NotFound();

            var teacher = _mapper.Map<TeacherReadDto>(_teacherRepository.GetTeacher(teacherId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(teacher);
        }



        /// <summary>
        /// Post request
        /// </summary>
        /// <param name="teacherCreate"></param>
        /// <returns></returns> 
        //Post teacher
        [HttpPost]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTeacher([FromBody] TeacherCreateDto teacherCreate)
        {
            if (teacherCreate == null) return BadRequest(ModelState);

            if (_teacherRepository.TeacherExists(teacherCreate.TeacherId))
            {
                ModelState.AddModelError("", "Pupil already exists on this register.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var teacherMap = _mapper.Map<Teacher>(teacherCreate);

            if (!_teacherRepository.CreateTeacher(teacherMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }


        /// <summary>
        /// Put request
        /// </summary>
        /// <param name="teacherId"></param>
        /// <param name="teacherUpdate"></param>
        /// <returns></returns>
        //Put teacher
        [HttpPut("{teacherId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTeacher(Guid teacherId, [FromBody] TeacherUpdateDto teacherUpdate)
        {
            if (teacherUpdate == null) return BadRequest(ModelState);

            if (teacherId != teacherUpdate.TeacherId)
            {
                ModelState.AddModelError("IdMissMatch", "Update Id and Body Id must match.");
                return BadRequest(ModelState);
            }

            if (!_teacherRepository.TeacherExists(teacherId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var teacherMap = _mapper.Map<Teacher>(teacherUpdate);

            if (!_teacherRepository.UpdateTeacher(teacherMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        /// <summary>
        /// Delete request
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        //delete teacher
        [HttpDelete("{teacherId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTeacher(Guid teacherId)
        {
            if (!_teacherRepository.TeacherExists(teacherId)) return NotFound();

            var teacherToDelete = _teacherRepository.GetTeacher(teacherId);

            var teacherAddressToDelete = _teacherAddressRepository.GetTeacherAddresses().ToList().Where(ta => ta.TeacherAddressTeacherId == teacherId).FirstOrDefault();

            var teacherContactToDelete = _teacherContactRepository.GetTeacherContacts().ToList().Where(tc => tc.TeacherContactTeacherId == teacherId).FirstOrDefault();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (teacherAddressToDelete != null)
            {
                if (!_teacherAddressRepository.DeleteTeacherAddress(teacherAddressToDelete))
                {
                    ModelState.AddModelError("", "Something went wrong while deleting the TeacherAddress belonging to this Teacher");
                    return StatusCode(500, ModelState);
                }
            }
            
            if (teacherContactToDelete != null)
            {
                if (!_teacherContactRepository.DeleteTeacherContact(teacherContactToDelete))    
                {
                    ModelState.AddModelError("", "Something went wrong while deleting the TeacherContact belonging to this Teacher");
                    return StatusCode(500, ModelState);
                }
            }

            if (!_teacherRepository.DeleteTeacher(teacherToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting the Teacher");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
