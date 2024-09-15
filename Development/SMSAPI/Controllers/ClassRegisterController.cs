using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMSAPI.Dto.ClassRegisterDto;
using SMSAPI.Dto.Register;
using SMSAPI.Interfaces;
using SMSAPI.Models;
using SMSAPI.Repository;

namespace SMSAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class ClassRegisterController : Controller
    {
        private readonly IClassRegisterRepository _classRegisterRepository;
        private readonly IMapper _mapper;

        public ClassRegisterController(IClassRegisterRepository classRegisterRepository, IMapper mapper)
        {
            _classRegisterRepository = classRegisterRepository;
            _mapper = mapper;
        }



        /// <summary>
        /// http get requests
        /// </summary>
        /// <returns></returns>
        //get regular collection of classregisters
        [HttpGet]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ClassRegister>))]
        public IActionResult GetClassRegisters()
        {
            var classRegisters = _mapper.Map<List<ClassRegisterReadDto>>(_classRegisterRepository.GetClassRegisters()); 

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(classRegisters);
        }

        //get classregister by classid
        [HttpGet("ClassRegister/Id/{classRegisterId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ClassRegister>))]
        [ProducesResponseType(400)]
        public IActionResult GetClassRegisterByClassId(Guid classRegisterId)
        {
            if (!_classRegisterRepository.ClassRoomRegisterExists(classRegisterId)) return NotFound();

            var classRegister = _mapper.Map<ClassRegisterReadDto>(_classRegisterRepository.GetClassRegisterByClassId(classRegisterId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(classRegister);
        }




        /// <summary>
        /// Post request
        /// </summary>
        /// <param name="classRegisterCreate"></param>
        /// <returns></returns> 
        //Post classRegister
        [HttpPost]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateClassRegister([FromBody] ClassRegisterCreateDto classRegisterCreate)
        {
            if (classRegisterCreate == null) return BadRequest(ModelState);

            if (_classRegisterRepository.ClassRoomRegisterExists(classRegisterCreate.ClassRegisterId))
            {
                ModelState.AddModelError("", "Pupil already exists on this register.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var classRegisterMap = _mapper.Map<ClassRegister>(classRegisterCreate);

            if (!_classRegisterRepository.CreateClassRegister(classRegisterMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }


        /// <summary>
        /// Put request
        /// </summary>
        /// <param name="classRegisterId"></param>
        /// <param name="classRegisterUpdate"></param>
        /// <returns></returns>
        //Put classRegister
        [HttpPut("{classRegisterId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateClassRegister(Guid classRegisterId, [FromBody] ClassRegisterUpdateDto classRegisterUpdate)
        {
            if (classRegisterUpdate == null) return BadRequest(ModelState);

            if (classRegisterId != classRegisterUpdate.ClassRegisterId)
            {
                ModelState.AddModelError("IdMissMatch", "Update Id and Body Id must match.");
                return BadRequest(ModelState);
            }

            if (!_classRegisterRepository.ClassRoomRegisterExists(classRegisterId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var classRegisterMap = _mapper.Map<ClassRegister>(classRegisterUpdate);

            var classDetails = _classRegisterRepository.GetClassRegisterByClassId(classRegisterMap.ClassRegisterId);

            classRegisterMap.ClassRegisterName = classDetails.ClassRegisterName;
            classRegisterMap.ClassRegisterGrade = classDetails.ClassRegisterGrade;

            if (!_classRegisterRepository.UpdateClassRegister(classRegisterMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        /// <summary>
        /// Delete request
        /// </summary>
        /// <param name="classRegisterId"></param>
        /// <returns></returns>
        //delete classRegister
        [HttpDelete("{classRegisterId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteClassRegister(Guid classRegisterId)
        {
            if (!_classRegisterRepository.ClassRoomRegisterExists(classRegisterId)) return NotFound();

            var classRegisterToDelete = _classRegisterRepository.GetClassRegisterByClassId(classRegisterId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_classRegisterRepository.DeleteClassRegister(classRegisterToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting the ClassRegister");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
