using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMSAPI.Dto.ClassRoomDto;
using SMSAPI.Dto.GuardianContactDto;
using SMSAPI.Dto.PupilDto;
using SMSAPI.Dto.SubjectDto;
using SMSAPI.Interfaces;
using SMSAPI.Models;
using SMSAPI.Repository;

namespace SMSAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class GuardianContactController : Controller
    {
        private readonly IGuardianContactRepository _guardianContactRepository;
        private readonly IMapper _mapper;

        public GuardianContactController(IGuardianContactRepository guardianContactRepository, IMapper mapper)
        {
            _guardianContactRepository = guardianContactRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// http request
        /// </summary>
        /// <returns></returns>
        //Get regular collection of guardiancontacts
        [HttpGet]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GuardianContact>))]
        public IActionResult GetGuardianContacts()
        {
            var guardianContacts = _mapper.Map<List<GuardianContactReadDto>>(_guardianContactRepository.GetGuardianContacts());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(guardianContacts);
        }

        //Get guardianid by guardiancontactid
        [HttpGet("Guardian/{guardianContactId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GuardianContact>))]
        [ProducesResponseType(400)]
        public IActionResult GetGuardianIdByGuardianContactId(Guid guardianContactId)
        {
            if (!_guardianContactRepository.GuardianContactExists(guardianContactId)) return NotFound();

            var guardianId = _guardianContactRepository.GetGuardianIdByGuardianContactId(guardianContactId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(guardianId);
        }

        //Get guardiancontact
        [HttpGet("GuardianContact/Id/{guardianContactId}")]
        //[ServiceFilter(typeof(AuthFilter))]   
        [ProducesResponseType(200, Type = typeof(Guid))]
        [ProducesResponseType(400)]
        public IActionResult GetGuardianContactId(Guid guardianContactId)
        {
            if (!_guardianContactRepository.GuardianContactExists(guardianContactId)) return NotFound();

            var guardianContact = _guardianContactRepository.GetGuardianContact(guardianContactId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(guardianContact);
        }



        /// <summary>
        /// Post request
        /// </summary>
        /// <param name="guardianContactCreate"></param>
        /// <returns></returns>
        //Post classroom
        [HttpPost]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateGuardianContact([FromBody] GuardianContactCreateDto guardianContactCreate)
        {
            if (guardianContactCreate == null) return BadRequest(ModelState);

            if (_guardianContactRepository.GuardianContactExists(guardianContactCreate.GuardianContactId))
            {
                ModelState.AddModelError("", "ClassRoom already exists.");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var guardianContactMap = _mapper.Map<GuardianContact>(guardianContactCreate);

            if (!_guardianContactRepository.CreateGuardianContact(guardianContactMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }


        /// <summary>
        /// Put request
        /// </summary>
        /// <param name="guardianContactId"></param>
        /// <param name="guardianContactUpdate"></param>
        /// <returns></returns>
        //Put classroom
        [HttpPut("{guardianContactId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateGuardianContact(Guid guardianContactId, [FromBody] GuardianContactUpdateDto guardianContactUpdate)
        {
            if (guardianContactUpdate == null) return BadRequest(ModelState);

            if (guardianContactId != guardianContactUpdate.GuardianContactId)
            {
                ModelState.AddModelError("IdMissMatch", "Update Id and Body Id must match.");
                return BadRequest(ModelState);
            }

            if (!_guardianContactRepository.GuardianContactExists(guardianContactId)) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var guardianContactMap = _mapper.Map<GuardianContact>(guardianContactUpdate);

            guardianContactMap.GuardianContactGuardianId = _guardianContactRepository.GetGuardianIdByGuardianContactId(guardianContactId);

            if (!_guardianContactRepository.UpdateGuardianContact(guardianContactMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        /// <summary>
        /// Delete request
        /// </summary>
        /// <param name="guardianContactId"></param>
        /// <returns></returns>
        //delete classroom
        [HttpDelete("{guardianContactId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteGuardianContact(Guid GuardianContactId)
        {
            if (!_guardianContactRepository.GuardianContactExists(GuardianContactId)) return NotFound();

            var guardianContactToDelete = _guardianContactRepository.GetGuardianContact(GuardianContactId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_guardianContactRepository.DeleteGuardianContact(guardianContactToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting the classroom");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
