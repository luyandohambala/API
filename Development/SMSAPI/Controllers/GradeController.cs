using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMSAPI.Dto.ClassRoomDto;
using SMSAPI.Dto.GradeDto;
using SMSAPI.Dto.PupilDto;
using SMSAPI.Dto.SubjectDto;
using SMSAPI.Interfaces;
using SMSAPI.Models;
using SMSAPI.Repository;

namespace SMSAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class GradeController : Controller
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly IClassRoomRepository _classRoomRepository;
        private readonly IMapper _mapper;

        public GradeController(IGradeRepository gradeRepository, IClassRoomRepository classRoomRepository, IMapper mapper)
        {
            _gradeRepository = gradeRepository;
            _classRoomRepository = classRoomRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// http get requests
        /// </summary>
        /// <returns></returns>
        //get regular collection of grades
        [HttpGet]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Grade>))]
        public IActionResult GetGrades()
        {
            var grades = _mapper.Map<List<GradeReadDto>>(_gradeRepository.GetGrades());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(grades);
        }

        //Get collection of classrooms from a grade
        [HttpGet("ClassRooms/{gradeId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ClassRoom>))]
        [ProducesResponseType(400)]
        public IActionResult GetClassRoomsByGradeId(Guid gradeId)
        {
            if (!_gradeRepository.GradeExists(gradeId)) return NotFound();

            var classRooms = _mapper.Map<List<SubjectReadDto>>(_gradeRepository.GetClassRoomsByGradeId(gradeId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(classRooms);
        }

        //Get grade by gradeId
        [HttpGet("Grade/Id/{gradeId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(Grade))]
        [ProducesResponseType(400)]
        public IActionResult GetPupilsByClassRoomId(Guid gradeId)
        {
            if (!_gradeRepository.GradeExists(gradeId)) return NotFound();

            var grade = _mapper.Map<GradeReadDto>(_gradeRepository.GetGrade(gradeId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(grade);
        }



        /// <summary>
        /// Post request
        /// </summary>
        /// <param name="gradeCreate"></param>
        /// <returns></returns>
        //Post grade
        [HttpPost]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateClassRoom([FromBody] GradeCreateDto gradeCreate)
        {
            if (gradeCreate == null) return BadRequest(ModelState);

            if (_gradeRepository.GradeExists(gradeCreate.GradeId))
            {
                ModelState.AddModelError("", "Grade already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var gradeMap = _mapper.Map<Grade>(gradeCreate);

            if (!_gradeRepository.CreateGrade(gradeMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }


        /// <summary>
        /// Put request
        /// </summary>
        /// <param name="gradeId"></param>
        /// <param name="gradeUpdate"></param>
        /// <returns></returns>
        //Put grade
        [HttpPut("{gradeId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateClassRoom(Guid gradeId, [FromBody] GradeUpdateDto gradeUpdate)
        {
            if (gradeUpdate == null) return BadRequest(ModelState);

            if (gradeId != gradeUpdate.GradeId)
            {
                ModelState.AddModelError("IdMissMatch", "Update Id and Body Id must match.");
                return BadRequest(ModelState);
            }

            if (!_gradeRepository.GradeExists(gradeId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var gradeMap = _mapper.Map<Grade>(gradeUpdate);

            if (!_gradeRepository.UpdateGrade(gradeMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        /// <summary>
        /// Delete request
        /// </summary>
        /// <param name="gradeId"></param>
        /// <returns></returns>
        //delete grade
        [HttpDelete("{gradeId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteGrade(Guid gradeId)
        {
            if (!_gradeRepository.GradeExists(gradeId)) return NotFound();

            var gradeToDelete = _gradeRepository.GetGrade(gradeId);

            var classRoomsToDelete = _gradeRepository.GetClassRoomsByGradeId(gradeId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (classRoomsToDelete.Count != 0)
            {
                if (!_classRoomRepository.DeleteClassRooms([.. classRoomsToDelete]))
                {
                    ModelState.AddModelError("", "Something went wrong while deleting the ClassRooms belonging to this Grade");
                    return StatusCode(500, ModelState);
                }
            }

            if (!_gradeRepository.DeleteGrade(gradeToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting the Grade");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
