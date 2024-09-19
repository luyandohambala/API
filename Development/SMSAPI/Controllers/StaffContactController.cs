using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMSAPI.Dto.StaffContactDto;
using SMSAPI.Interfaces;
using SMSAPI.Models;

namespace SMSAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class StaffContactController : Controller
    {
        private readonly IStaffContactRepository _staffContactRepository;
        private readonly IMapper _mapper;

        public StaffContactController(IStaffContactRepository StaffContactRepository, IMapper mapper)
        {
            _staffContactRepository = StaffContactRepository;
            _mapper = mapper;
        }



        /// <summary>
        /// http request
        /// </summary>
        /// <returns></returns>
        //Get regular collection of StaffContacts
        [HttpGet]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<StaffContact>))]
        public IActionResult GetStaffContacts()
        {
            var StaffContacts = _mapper.Map<List<StaffContactReadDto>>(_staffContactRepository.GetStaffContacts());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(StaffContacts);
        }

        //Get Staffid by staffContactId
        [HttpGet("Staff/{staffContactId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<StaffContact>))]
        [ProducesResponseType(400)]
        public IActionResult GetGuardianIdBystaffContactId(Guid staffContactId)
        {
            if (!_staffContactRepository.StaffContactExists(staffContactId)) return NotFound();

            var staffId = _staffContactRepository.GetStaffIdByStaffContactId(staffContactId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(staffId);
        }

        //Get StaffContact
        [HttpGet("StaffContact/Id/{staffContactId}")]
        //[ServiceFilter(typeof(AuthFilter))]   
        [ProducesResponseType(200, Type = typeof(Guid))]
        [ProducesResponseType(400)]
        public IActionResult GetstaffContactId(Guid staffContactId)
        {
            if (!_staffContactRepository.StaffContactExists(staffContactId)) return NotFound();

            var staffContact = _staffContactRepository.GetStaffContact(staffContactId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(staffContact);
        }



        /// <summary>
        /// Post request
        /// </summary>
        /// <param name="staffContactCreate"></param>
        /// <returns></returns>
        //Post Staffcontact 
        [HttpPost]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateStaffContact([FromBody] StaffContactCreateDto staffContactCreate)
        {
            if (staffContactCreate == null) return BadRequest(ModelState);

            if (_staffContactRepository.StaffContactExists(staffContactCreate.StaffContactId))
            {
                ModelState.AddModelError("", "StaffContact already exists.");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var staffContactMap = _mapper.Map<StaffContact>(staffContactCreate);

            if (!_staffContactRepository.CreateStaffContact(staffContactMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }


        /// <summary>
        /// Put request
        /// </summary>
        /// <param name="staffContactId"></param>
        /// <param name="StaffContactUpdate"></param>
        /// <returns></returns>
        //Put Staffcontact
        [HttpPut("{staffContactId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateStaffContact(Guid staffContactId, [FromBody] StaffContactUpdateDto StaffContactUpdate)
        {
            if (StaffContactUpdate == null) return BadRequest(ModelState);

            if (staffContactId != StaffContactUpdate.StaffContactId)
            {
                ModelState.AddModelError("IdMissMatch", "Update Id and Body Id must match.");
                return BadRequest(ModelState);
            }

            if (!_staffContactRepository.StaffContactExists(staffContactId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var staffContactMap = _mapper.Map<StaffContact>(StaffContactUpdate);

            staffContactMap.StaffContactStaffId = _staffContactRepository.GetStaffIdByStaffContactId(staffContactId);

            if (!_staffContactRepository.UpdateStaffContact(staffContactMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        /// <summary>
        /// Delete request
        /// </summary>
        /// <param name="staffContactId"></param>
        /// <returns></returns>
        //delete Staffcontact
        [HttpDelete("{staffContactId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteStaffContact(Guid staffContactId)
        {
            if (!_staffContactRepository.StaffContactExists(staffContactId)) return NotFound();

            var staffContactToDelete = _staffContactRepository.GetStaffContact(staffContactId);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_staffContactRepository.DeleteStaffContact(staffContactToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting the StaffContact");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
