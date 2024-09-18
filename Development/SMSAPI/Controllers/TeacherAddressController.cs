using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMSAPI.Dto.TeacherAddressDto;
using SMSAPI.Interfaces;
using SMSAPI.Models;

namespace SMSAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class TeacherAddressController : Controller
    {
        private readonly ITeacherAddressRepository _teacherAddressRepository;
        private readonly IMapper _mapper;

        public TeacherAddressController(ITeacherAddressRepository teacherAddressRepository, IMapper mapper)
        {
            _teacherAddressRepository = teacherAddressRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// http get requests 
        /// </summary>
        /// <returns></returns>
        //get regular collection of teacheraddress
        [HttpGet]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TeacherAddress>))]
        public IActionResult GetTeacherAdrresses()
        {
            var teacherAddresses = _mapper.Map<List<TeacherAddressReadDto>>(_teacherAddressRepository.GetTeacherAddresses());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(teacherAddresses);
        }

        //get teacheraddress by teacheraddressId
        [HttpGet("TeacherAddress/id/{teacherAddressId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(ClassRegister))]
        [ProducesResponseType(400)]
        public IActionResult GetTeacherAddress(Guid teacherAddressId)
        {
            if (!_teacherAddressRepository.TeacherAddressExists(teacherAddressId)) return NotFound();

            var teacherAddress = _mapper.Map<TeacherAddress>(_teacherAddressRepository.GetTeacherAddress(teacherAddressId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(teacherAddress);
        }

        //get teacher id from teacherAddressId
        [HttpGet("TeacherId/{teacherAddressId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(Guid))]
        [ProducesResponseType(400)]
        public IActionResult GetTeacherIdByTeacherAddressId(Guid teacherAddressId)
        {
            if (!_teacherAddressRepository.TeacherAddressExists(teacherAddressId)) return NotFound();

            var teacherId = _teacherAddressRepository.GetTeacherIdByTeacherAddressId(teacherAddressId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(teacherId);
        }


        /// <summary>
        /// Post request
        /// </summary>
        /// <param name="teacherAddressCreate"></param>
        /// <returns></returns> 
        //Post teacherAddress
        [HttpPost]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateteacherAddress([FromBody] TeacherAddressCreateDto teacherAddressCreate)
        {
            if (teacherAddressCreate == null) return BadRequest(ModelState);

            if (_teacherAddressRepository.TeacherAddressExists(teacherAddressCreate.TeacherAddressId))
            {
                ModelState.AddModelError("", "TeacherAdrress already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var teacherAddressMap = _mapper.Map<TeacherAddress>(teacherAddressCreate);

            if (!_teacherAddressRepository.CreateTeacherAddress(teacherAddressMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }


        /// <summary>
        /// Put request
        /// </summary>
        /// <param name="teacherAddressId"></param>
        /// <param name="teacherAddressUpdate"></param>
        /// <returns></returns>
        //Put teacherAddress
        [HttpPut("{teacherAddressId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateteacherAddress(Guid teacherAddressId, [FromBody] TeacherAddressUpdateDto teacherAddressUpdate)
        {
            if (teacherAddressUpdate == null) return BadRequest(ModelState);

            if (teacherAddressId != teacherAddressUpdate.TeacherAddressId)
            {
                ModelState.AddModelError("IdMissMatch", "Update Id and Body Id must match.");
                return BadRequest(ModelState);
            }

            if (!_teacherAddressRepository.TeacherAddressExists(teacherAddressId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var teacherAddressMap = _mapper.Map<TeacherAddress>(teacherAddressUpdate);

            teacherAddressMap.TeacherAddressTeacherId = _teacherAddressRepository.GetTeacherAddress(teacherAddressMap.TeacherAddressId).TeacherAddressTeacherId;

            if (!_teacherAddressRepository.UpdateTeacherAddress(teacherAddressMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        /// <summary>
        /// Delete request
        /// </summary>
        /// <param name="teacherAddressId"></param>
        /// <returns></returns>
        //delete teacherAddress
        [HttpDelete("{teacherAddressId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteteacherAddress(Guid teacherAddressId)
        {
            if (!_teacherAddressRepository.TeacherAddressExists(teacherAddressId)) return NotFound();

            var teacherAddressToDelete = _teacherAddressRepository.GetTeacherAddress(teacherAddressId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_teacherAddressRepository.DeleteTeacherAddress(teacherAddressToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting the TeacherAddress");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
