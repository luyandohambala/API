using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMSAPI.Dto.DepartmentDto;
using SMSAPI.Dto.StaffDto;
using SMSAPI.Dto.TeacherDto;
using SMSAPI.Interfaces;
using SMSAPI.Models;

namespace SMSAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmentRepository, IEquipmentRepository equipmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _equipmentRepository = equipmentRepository;
            _mapper = mapper;
        }



        /// <summary>
        /// http get requests
        /// </summary>
        /// <returns></returns>
        //get regualar collection of departments
        [HttpGet]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Department>))]
        public IActionResult GetDepartments()
        {
            var departments = _mapper.Map<List<DepartmentReadDto>>(_departmentRepository.GetDepartments());

            if (!ModelState.IsValid) return BadRequest(ModelState); 

            return Ok(departments);
        }

        //get collection of staff from a department
        [HttpGet("Staff/{departmentId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Staff>))]
        [ProducesResponseType(400)]
        public IActionResult GetStaffByDepartmentId(Guid departmentId)
        {
            if (!_departmentRepository.DepartmentExists(departmentId)) return NotFound();

            var staff = _mapper.Map<List<StaffReadDto>>(_departmentRepository.GetStaffByDepartmentId(departmentId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(staff);
        }
        
        //get collection of teachers from a department
        [HttpGet("Teachers/{departmentId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Teacher>))]
        [ProducesResponseType(400)]
        public IActionResult GetTeachersByDepartmentId(Guid departmentId)
        {
            if (!_departmentRepository.DepartmentExists(departmentId)) return NotFound();

            var teachers = _mapper.Map<List<TeacherReadDto>>(_departmentRepository.GetTeachersByDepartmentId(departmentId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(teachers);
        }



        //get department by departmentId
        [HttpGet("Department/Id/{departmentId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(Department))]
        [ProducesResponseType(400)]
        public IActionResult GetDepartment(Guid departmentId)
        {
            if (!_departmentRepository.DepartmentExists(departmentId)) return NotFound();

            var department = _mapper.Map<DepartmentReadDto>(_departmentRepository.GetDepartment(departmentId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(department);
        }




        /// <summary>
        /// Post request
        /// </summary>
        /// <param name="departmentCreate"></param>
        /// <returns></returns> 
        //Post department
        [HttpPost]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateDepartment([FromBody] DepartmentCreateDto departmentCreate)
        {
            if (departmentCreate == null) return BadRequest(ModelState);

            if (_departmentRepository.DepartmentExists(departmentCreate.DepartmentId))
            {
                ModelState.AddModelError("", "Department already exists.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var departmentMap = _mapper.Map<Department>(departmentCreate);

            if (!_departmentRepository.CreateDepartment(departmentMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }


        /// <summary>
        /// Put request
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="departmentUpdate"></param>
        /// <returns></returns>
        //Put department
        [HttpPut("{departmentId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDepartment(Guid departmentId, [FromBody] DepartmentUpdateDto departmentUpdate)
        {
            if (departmentUpdate == null) return BadRequest(ModelState);

            if (departmentId != departmentUpdate.DepartmentId)
            {
                ModelState.AddModelError("IdMissMatch", "Update Id and Body Id must match.");
                return BadRequest(ModelState);
            }

            if (!_departmentRepository.DepartmentExists(departmentId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var departmentMap = _mapper.Map<Department>(departmentUpdate);

            if (!_departmentRepository.UpdateDepartment(departmentMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        /// <summary>
        /// Delete request
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        //delete department
        [HttpDelete("{departmentId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDepartment(Guid departmentId)
        {
            if (!_departmentRepository.DepartmentExists(departmentId)) return NotFound();

            var departmentToDelete = _departmentRepository.GetDepartment(departmentId);

            var equipmentToDelete = _equipmentRepository.GetEquipment().ToList().Where(e => e.EquipmentDepartmentId == departmentId).FirstOrDefault();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (equipmentToDelete != null)
            {
                if (!_equipmentRepository.DeleteEquipment(equipmentToDelete))
                {
                    ModelState.AddModelError("", "Something went wrong while deleting the equipment belonging to this Department");
                    return StatusCode(500, ModelState);
                }
            }

            if (!_departmentRepository.DeleteDepartment(departmentToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting the Department");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
