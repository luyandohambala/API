using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMSAPI.Dto.ClassRoomDto;
using SMSAPI.Dto.GuardianContactDto;
using SMSAPI.Dto.GuardianDto;
using SMSAPI.Dto.PupilDto;
using SMSAPI.Dto.SubjectDto;
using SMSAPI.Interfaces;
using SMSAPI.Models;
using SMSAPI.Repository;

namespace SMSAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class GuardianController : Controller
    {
        private readonly IGuardianRepository _guardianRepository;
        private readonly IGuardianContactRepository _guardianContactRepository;
        private readonly IMapper _mapper;

        public GuardianController(IGuardianRepository guardianRepository, IGuardianContactRepository guardianContactRepository, IMapper mapper)
        {
            _guardianRepository = guardianRepository;
            _guardianContactRepository = guardianContactRepository;
            _mapper = mapper;
        }



        //Get regular collection of guardians
        [HttpGet]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Guardian>))]
        public IActionResult GetGuardians()
        {
            var guardians = _mapper.Map<List<GuardianReadDto>>(_guardianRepository.GetGuardians());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(guardians);
        }

        //Get single guardian
        [HttpGet("Guardian/{guardianId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(Guardian))]
        [ProducesResponseType(400)]
        public IActionResult GetGuardian(Guid guardianId)
        {
            if (!_guardianRepository.GuardianExists(guardianId)) return NotFound();

            var guardian = _mapper.Map<GuardianReadDto>(_guardianRepository.GetGuardian(guardianId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(guardian);
        }

        //Get collection of pupils by guardian Id
        [HttpGet("Pupils/{guardianId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pupil>))]
        [ProducesResponseType(400)]
        public IActionResult GetPupilsByGuardianId(Guid guardianId)
        {
            if (!_guardianRepository.GuardianExists(guardianId)) return NotFound();

            var pupils = _mapper.Map<List<PupilReadDto>>(_guardianRepository.GetPupilsByGuardianId(guardianId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(pupils);
        }

        //Get guardian contact by guardianId
        [HttpGet("GuardianContact/{guardianId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(GuardianContact))]
        [ProducesResponseType(400)]
        public IActionResult GetGuardianContactByGuardianId(Guid guardianId)
        {
            if (!_guardianRepository.GuardianExists(guardianId)) return NotFound();

            var guardianContact = _mapper.Map<GuardianContactReadDto>(_guardianRepository.GetGuardianContactByGuardianId(guardianId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(guardianContact);
        }



        /// <summary>
        /// Post request
        /// </summary>
        /// <param name="guardianCreate"></param>
        /// <returns></returns>
        //Post classroom
        [HttpPost]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateGuardian([FromBody] GuardianCreateDto guardianCreate)
        {
            if (guardianCreate == null) return BadRequest(ModelState);

            if (_guardianRepository.GuardianExists(guardianCreate.GuardianId))
            {
                ModelState.AddModelError("", "Guardian already exists.");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var guardianMap = _mapper.Map<Guardian>(guardianCreate);

            if (!_guardianRepository.CreateGuardian(guardianMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }


        /// <summary>
        /// Put request
        /// </summary>
        /// <param name="guardianId"></param>
        /// <param name="guardianUpdate"></param>
        /// <returns></returns>
        //Put classroom
        [HttpPut("{guardianId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateGuardian(Guid guardianId, [FromBody] GuardianUpdateDto guardianUpdate)
        {
            if (guardianUpdate == null) return BadRequest(ModelState);

            if (guardianId != guardianUpdate.GuardianId)
            {
                ModelState.AddModelError("IdMissMatch", "Update Id and Body Id must match.");
                return BadRequest(ModelState);
            }

            if (!_guardianRepository.GuardianExists(guardianId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var guardianMap = _mapper.Map<Guardian>(guardianUpdate);

            if (!_guardianRepository.UpdateGuardian(guardianMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        /// <summary>
        /// Delete request
        /// </summary>
        /// <param name="guardianId"></param>
        /// <returns></returns>
        //delete classroom
        [HttpDelete("{guardianId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteGuardian(Guid guardianId)
        {
            if (!_guardianRepository.GuardianExists(guardianId)) return NotFound();

            var guardianToDelete = _guardianRepository.GetGuardian(guardianId);

            var guardianContactToDelete = _guardianRepository.GetGuardianContactByGuardianId(guardianId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (guardianContactToDelete != null)
            {
                if (!_guardianContactRepository.DeleteGuardianContact(guardianContactToDelete))
                {
                    ModelState.AddModelError("", "Something went wrong while deleting the Contact belonging to this Guardian");
                    return StatusCode(500, ModelState);
                }
            }

            if (!_guardianRepository.DeleteGuardian(guardianToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting the classroom");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
