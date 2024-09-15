using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMSAPI.Authentication;
using SMSAPI.Dto.ClassRoomDto;
using SMSAPI.Dto.PupilDto;
using SMSAPI.Dto.SubjectDto;
using SMSAPI.Interfaces;
using SMSAPI.Models;

namespace SMSAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class ClassRoomController : Controller
    {
        private readonly IClassRoomRepository _classRoomRepository;
        private readonly IPupilRepository _pupilRepository;
        private readonly IMapper _mapper;

        public ClassRoomController(IClassRoomRepository classRoomRepository, IPupilRepository pupilRepository, IMapper mapper)
        {
            _classRoomRepository = classRoomRepository;
            _pupilRepository = pupilRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// Get requests
        /// </summary>
        /// <returns></returns>
        //Get regular collection of classrooms
        [HttpGet]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ClassRoom>))]
        public IActionResult GetClassRooms()
        {
            var classRooms = _mapper.Map<List<ClassRoomReadDto>>(_classRoomRepository.GetClassRooms());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(classRooms);
        }

        //Get collection of subjects from a classroom
        [HttpGet("Subjects/{classRoomId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Subject>))]
        [ProducesResponseType(400)]
        public IActionResult GetSubjectsByClassRoomId(Guid classRoomId)
        {
            if (!_classRoomRepository.ClassRoomExists(classRoomId)) return NotFound();

            var subjects = _mapper.Map<List<SubjectReadDto>>(_classRoomRepository.GetSubjectsByClassRoomId(classRoomId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(subjects);
        }

        //Get collection of pupils from a classroom
        [HttpGet("Pupils/{classRoomId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pupil>))]
        [ProducesResponseType(400)]
        public IActionResult GetPupilsByClassRoomId(Guid classRoomId)
        {
            if (!_classRoomRepository.ClassRoomExists(classRoomId)) return NotFound();

            var pupils = _mapper.Map<List<PupilReadDto>>(_classRoomRepository.GetPupilsByClassRoomId(classRoomId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(pupils);
        }

        //Get classroom by Id with subjects
        [HttpGet("WithSubjects/Id/{classRoomId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(ClassRoom))]
        [ProducesResponseType(400)]
        public IActionResult GetClassRoomWithSubjects(Guid classRoomId)
        {
            if (!_classRoomRepository.ClassRoomExists(classRoomId)) return NotFound();

            var classRoom = _mapper.Map<ClassRoomWithSubjectsDto>(_classRoomRepository.GetClassRoom(classRoomId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(classRoom);
        }

        //Get classroom by Id with pupils
        [HttpGet("WithPupils/Id/{classRoomId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(ClassRoom))]
        [ProducesResponseType(400)]
        public IActionResult GetClassRoomWithPupils(Guid classRoomId)
        {
            if (!_classRoomRepository.ClassRoomExists(classRoomId)) return NotFound();

            var classRoom = _mapper.Map<ClassRoomWithPupilsDto>(_classRoomRepository.GetClassRoom(classRoomId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(classRoom);
        }

        //Get gradeId of classroom
        [HttpGet("GradeId/{classRoomId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(Guid))]
        [ProducesResponseType(400)]
        public IActionResult GetGradeIdByClassRoomId(Guid classRoomId)
        {
            if (!_classRoomRepository.ClassRoomExists(classRoomId)) return NotFound();

            var gradeId = _classRoomRepository.GetGradeByClassRoomId(classRoomId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(gradeId);
        }



        /// <summary>
        /// Post request
        /// </summary>
        /// <param name="classRoomCreate"></param>
        /// <returns></returns>
        //Post classroom
        [HttpPost]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateClassRoom([FromBody] ClassRoomCreateDto classRoomCreate)
        {
            if (classRoomCreate == null) return BadRequest(ModelState);

            if (_classRoomRepository.ClassRoomExists(classRoomCreate.ClassId))
            {
                ModelState.AddModelError("", "ClassRoom already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var classRoomMap = _mapper.Map<ClassRoom>(classRoomCreate);

            if (!_classRoomRepository.CreateClassRoom(classRoomMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }


        /// <summary>
        /// Put request
        /// </summary>
        /// <param name="classRoomId"></param>
        /// <param name="classRoomUpdate"></param>
        /// <returns></returns>
        //Put classroom
        [HttpPut("{classRoomId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateClassRoom(Guid classRoomId, [FromBody] ClassRoomUpdateDto classRoomUpdate)
        {
            if (classRoomUpdate == null) return BadRequest(ModelState);

            if (classRoomId != classRoomUpdate.ClassId)
            {
                ModelState.AddModelError("IdMissMatch", "Update Id and Body Id must match.");
                return BadRequest(ModelState);
            }

            if (!_classRoomRepository.ClassRoomExists(classRoomId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var classRoomMap = _mapper.Map<ClassRoom>(classRoomUpdate);

            classRoomMap.ClassGradeId = _classRoomRepository.GetGradeByClassRoomId(classRoomId);

            classRoomMap.PupilTotal = _classRoomRepository.GetClassRoom(classRoomMap.ClassId).PupilTotal;

            if (!_classRoomRepository.UpdateClassRoom(classRoomMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        /// <summary>
        /// Delete request
        /// </summary>
        /// <param name="classRoomId"></param>
        /// <returns></returns>
        //delete classroom
        [HttpDelete("{classRoomId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteClassRoom(Guid classRoomId)
        {
            if (!_classRoomRepository.ClassRoomExists(classRoomId)) return NotFound();

            var classRoomToDelete = _classRoomRepository.GetClassRoom(classRoomId);

            var pupilsToDelete = _classRoomRepository.GetPupilsByClassRoomId(classRoomId);  

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (pupilsToDelete.Count != 0)
            {
                if (!_pupilRepository.DeletePupils([.. pupilsToDelete]))
                {
                    ModelState.AddModelError("", "Something went wrong while deleting the Pupils belonging to this ClassRoom");
                    return StatusCode(500, ModelState);
                }
            }

            if (!_classRoomRepository.DeleteClassRoom(classRoomToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting the classroom");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
