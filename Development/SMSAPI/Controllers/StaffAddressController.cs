using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMSAPI.Dto.StaffAddressDto;
using SMSAPI.Interfaces;
using SMSAPI.Models;

namespace SMSAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class StaffAddressController : Controller
    {
        private readonly IStaffAddressRepository _staffAddressRepository;
        private readonly IMapper _mapper;

        public StaffAddressController(IStaffAddressRepository staffAddressRepository, IMapper mapper)
        {
            _staffAddressRepository = staffAddressRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// http get requests 
        /// </summary>
        /// <returns></returns>
        //get regular collection of staffaddress
        [HttpGet]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<StaffAddress>))]
        public IActionResult GetStaffAdrresses()
        {
            var staffAddresses = _mapper.Map<List<StaffAddressReadDto>>(_staffAddressRepository.GetStaffAddresses());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(staffAddresses);
        }

        //get staffaddress by staffaddressId
        [HttpGet("StaffAddress/id/{staffAddressId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(StaffAddress))]
        [ProducesResponseType(400)]
        public IActionResult GetStaffAddress(Guid staffAddressId)
        {
            if (!_staffAddressRepository.StaffAddressExists(staffAddressId)) return NotFound();

            var staffAddress = _mapper.Map<StaffAddress>(_staffAddressRepository.GetStaffAddress(staffAddressId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(staffAddress);
        }

        //get staff id from staffAddressId
        [HttpGet("StaffId/{staffAddressId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(Guid))]
        [ProducesResponseType(400)]
        public IActionResult GetStaffIdByStaffAddressId(Guid staffAddressId)
        {
            if (!_staffAddressRepository.StaffAddressExists(staffAddressId)) return NotFound();

            var staffId = _staffAddressRepository.GetStaffIdByStaffAddressId(staffAddressId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(staffId);
        }


        /// <summary>
        /// Post request
        /// </summary>
        /// <param name="staffAddressCreate"></param>
        /// <returns></returns> 
        //Post staffAddress
        [HttpPost]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatestaffAddress([FromBody] StaffAddressCreateDto staffAddressCreate)
        {
            if (staffAddressCreate == null) return BadRequest(ModelState);

            if (_staffAddressRepository.StaffAddressExists(staffAddressCreate.StaffAddressId))
            {
                ModelState.AddModelError("", "StaffAdrress already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var staffAddressMap = _mapper.Map<StaffAddress>(staffAddressCreate);

            if (!_staffAddressRepository.CreateStaffAddress(staffAddressMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }


        /// <summary>
        /// Put request
        /// </summary>
        /// <param name="staffAddressId"></param>
        /// <param name="staffAddressUpdate"></param>
        /// <returns></returns>
        //Put staffAddress
        [HttpPut("{staffAddressId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateStaffAddress(Guid staffAddressId, [FromBody] StaffAddressUpdateDto staffAddressUpdate)
        {
            if (staffAddressUpdate == null) return BadRequest(ModelState);

            if (staffAddressId != staffAddressUpdate.StaffAddressId)
            {
                ModelState.AddModelError("IdMissMatch", "Update Id and Body Id must match.");
                return BadRequest(ModelState);
            }

            if (!_staffAddressRepository.StaffAddressExists(staffAddressId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var staffAddressMap = _mapper.Map<StaffAddress>(staffAddressUpdate);

            staffAddressMap.StaffAddressStaffId = _staffAddressRepository.GetStaffAddress(staffAddressMap.StaffAddressId).StaffAddressStaffId;

            if (!_staffAddressRepository.UpdateStaffAddress(staffAddressMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        /// <summary>
        /// Delete request
        /// </summary>
        /// <param name="staffAddressId"></param>
        /// <returns></returns>
        //delete staffAddress
        [HttpDelete("{staffAddressId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteStaffAddress(Guid staffAddressId)
        {
            if (!_staffAddressRepository.StaffAddressExists(staffAddressId)) return NotFound();

            var staffAddressToDelete = _staffAddressRepository.GetStaffAddress(staffAddressId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_staffAddressRepository.DeleteStaffAddress(staffAddressToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting the StaffAddress");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
