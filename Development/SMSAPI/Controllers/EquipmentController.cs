using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMSAPI.Dto.ClassRegisterDto;
using SMSAPI.Dto.EquipmentDto;
using SMSAPI.Interfaces;
using SMSAPI.Models;
using SMSAPI.Repository;

namespace SMSAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class EquipmentController : Controller
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IMapper _mapper;

        public EquipmentController(IEquipmentRepository equipmentRepository, IMapper mapper)
        {
            _equipmentRepository = equipmentRepository;
            _mapper = mapper;
        }



        /// <summary>
        /// http get requests
        /// </summary>
        /// <returns></returns>
        //get regular collection of equipment
        [HttpGet]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Equipment>))]
        public IActionResult GetEquipment()
        {
            var equipment = _mapper.Map<List<EquipmentReadDto>>(_equipmentRepository.GetEquipment());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(equipment);
        }

        //get equipment  by equipmentid 
        [HttpGet("Equipment/Id/{equipmentId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(Equipment))]
        [ProducesResponseType(400)]
        public IActionResult GetClassRegisterByClassId(Guid equipmentId)
        {
            if (!_equipmentRepository.EquipmentExists(equipmentId)) return NotFound();

            var equipment = _mapper.Map<EquipmentReadDto>(_equipmentRepository.GetEquipmentById(equipmentId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(equipment);
        }

        [HttpGet("DepartmentId/{equipmentId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(Guid))]
        [ProducesResponseType(400)]
        public IActionResult GetDepartmentIdByEquipmentId(Guid equipmentId)
        {
            if (!_equipmentRepository.EquipmentExists(equipmentId)) return NotFound();

            var departmentId = _equipmentRepository.GetDepartmentIdByEquipmentId(equipmentId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(departmentId);
        }


        /// <summary>
        /// Post request
        /// </summary>
        /// <param name="equipmentCreate"></param>
        /// <returns></returns> 
        //Post Equipment
        [HttpPost]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateClassRegister([FromBody] EquipmentCreateDto equipmentCreate)
        {
            if (equipmentCreate == null) return BadRequest(ModelState);

            if (_equipmentRepository.EquipmentExists(equipmentCreate.EquipmentId))
            {
                ModelState.AddModelError("", "Equipment already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var equipmentMap = _mapper.Map<Equipment>(equipmentCreate);

            if (!_equipmentRepository.CreateEquipment(equipmentMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }


        /// <summary>
        /// Put request
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="equipmentUpdate"></param>
        /// <returns></returns>
        //Put Equipment
        [HttpPut("{equipmentId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateEquipment(Guid equipmentId, [FromBody] EquipmentUpdateDto equipmentUpdate)
        {
            if (equipmentUpdate == null) return BadRequest(ModelState);

            if (equipmentId != equipmentUpdate.EquipmentId)
            {
                ModelState.AddModelError("IdMissMatch", "Update Id and Body Id must match.");
                return BadRequest(ModelState);
            }

            if (!_equipmentRepository.EquipmentExists(equipmentId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var equipmentMap = _mapper.Map<Equipment>(equipmentUpdate);

            if (!_equipmentRepository.UpdateEquipment(equipmentMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        /// <summary>
        /// Delete request
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        //delete Equipment
        [HttpDelete("{equipmentId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteEquipment(Guid equipmentId)
        {
            if (!_equipmentRepository.EquipmentExists(equipmentId)) return NotFound();

            var equipmentToDelete = _equipmentRepository.GetEquipmentById(equipmentId);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_equipmentRepository.DeleteEquipment(equipmentToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting the Equipment");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
