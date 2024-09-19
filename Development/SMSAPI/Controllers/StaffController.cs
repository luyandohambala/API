using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMSAPI.Dto.StaffDto;
using SMSAPI.Dto.SubjectDto;
using SMSAPI.Interfaces;
using SMSAPI.Models;

namespace SMSAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class StaffController : Controller
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IStaffAddressRepository _staffAddressRepository;
        private readonly IStaffContactRepository _staffContactRepository;
        private readonly IMapper _mapper;

        public StaffController(IStaffRepository staffRepository, IStaffAddressRepository staffAddressRepository, IStaffContactRepository staffContactRepository, IMapper mapper)
        {
            _staffRepository = staffRepository;
            _staffAddressRepository = staffAddressRepository;
            _staffContactRepository = staffContactRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// http get requests
        /// </summary>
        /// <returns></returns>
        //get regular collection of Staff
        [HttpGet]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Staff>))]
        public IActionResult GetStaffs()
        {
            var staffs = _mapper.Map<List<StaffReadDto>>(_staffRepository.GetStaff());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(staffs);
        }

        [HttpGet("Staff/Id/{staffId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(Staff))]
        [ProducesResponseType(400)]
        public IActionResult GetStaff(Guid staffId)
        {
            if (!_staffRepository.StaffExists(staffId)) return NotFound();

            var Staff = _mapper.Map<StaffReadDto>(_staffRepository.GetStaffById(staffId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(Staff);
        }



        /// <summary>
        /// Post request
        /// </summary>
        /// <param name="staffCreate"></param>
        /// <returns></returns> 
        //Post Staff
        [HttpPost]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateStaff([FromBody] StaffCreateDto staffCreate)
        {
            if (staffCreate == null) return BadRequest(ModelState);

            if (_staffRepository.StaffExists(staffCreate.StaffId))
            {
                ModelState.AddModelError("", "Staff already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var staffMap = _mapper.Map<Staff>(staffCreate);

            if (!_staffRepository.CreateStaff(staffMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }


        /// <summary>
        /// Put request
        /// </summary>
        /// <param name="staffId"></param>
        /// <param name="staffUpdate"></param>
        /// <returns></returns>
        //Put Staff
        [HttpPut("{staffId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateStaff(Guid staffId, [FromBody] StaffUpdateDto staffUpdate)
        {
            if (staffUpdate == null) return BadRequest(ModelState);

            if (staffId != staffUpdate.StaffId)
            {
                ModelState.AddModelError("IdMissMatch", "Update Id and Body Id must match.");
                return BadRequest(ModelState);
            }

            if (!_staffRepository.StaffExists(staffId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var staffMap = _mapper.Map<Staff>(staffUpdate);

            if (!_staffRepository.UpdateStaff(staffMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        /// <summary>
        /// Delete request
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        //delete Staff
        [HttpDelete("{staffId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteStaff(Guid staffId)
        {
            if (!_staffRepository.StaffExists(staffId)) return NotFound();

            var staffToDelete = _staffRepository.GetStaffById(staffId);

            var staffAddressToDelete = _staffAddressRepository.GetStaffAddresses().ToList().Where(ta => ta.StaffAddressStaffId == staffId).FirstOrDefault();

            var staffContactToDelete = _staffContactRepository.GetStaffContacts().ToList().Where(tc => tc.StaffContactStaffId == staffId).FirstOrDefault();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (staffAddressToDelete != null)
            {
                if (!_staffAddressRepository.DeleteStaffAddress(staffAddressToDelete))
                {
                    ModelState.AddModelError("", "Something went wrong while deleting the StaffAddress belonging to this Staff");
                    return StatusCode(500, ModelState);
                }
            }

            if (staffContactToDelete != null)
            {
                if (!_staffContactRepository.DeleteStaffContact(staffContactToDelete))
                {
                    ModelState.AddModelError("", "Something went wrong while deleting the StaffContact belonging to this Staff");
                    return StatusCode(500, ModelState);
                }
            }

            if (!_staffRepository.DeleteStaff(staffToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting the Staff");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
