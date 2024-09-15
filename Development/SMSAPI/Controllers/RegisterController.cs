using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMSAPI.Dto.ClassRoomDto;
using SMSAPI.Dto.Register;
using SMSAPI.Interfaces;
using SMSAPI.Models;
using SMSAPI.Repository;

namespace SMSAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class RegisterController : Controller 
    {
        private readonly IRegisterRepository _registerRepository;
        private readonly IClassRegisterRepository _classRegisterRepository;
        private readonly IMapper _mapper;

        public RegisterController(IRegisterRepository registerRepository, IClassRegisterRepository classRegisterRepository, IMapper mapper)
        {
            _registerRepository = registerRepository;
            _classRegisterRepository = classRegisterRepository;
            _mapper = mapper;
        }



        /// <summary>
        /// http get requests
        /// </summary>
        /// <returns></returns>
        //get regular collection of pupil register
        [HttpGet]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Register>))]
        public IActionResult GetPupilsFromRegister()
        {
            var pupilsFromRegister = _mapper.Map<List<RegisterReadDto>>(_registerRepository.GetPupilsFromRegister());

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(pupilsFromRegister);
        }

        //get register pupils from a class
        [HttpGet("ClassRoom/{classRoomRegisterId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Register>))]
        [ProducesResponseType(400)]
        public IActionResult GetRegisterByClassId(Guid classRoomRegisterId)
        {
            if (!_classRegisterRepository.ClassRoomRegisterExists(classRoomRegisterId)) return NotFound();

            var pupilRegister = _mapper.Map<List<RegisterReadDto>>(_registerRepository.GetRegisterByClassId(classRoomRegisterId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(pupilRegister);
        }

        //get pupil from register by pupilid
        [HttpGet("Register/Id/{pupilRegisterId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ClassRoom>))]
        [ProducesResponseType(400)]
        public IActionResult GetPupilFromRegister(Guid pupilRegisterId)
        {
            if (!_registerRepository.RegisterPupilExists(pupilRegisterId)) return NotFound();

            var pupil = _mapper.Map<RegisterReadDto>(_registerRepository.GetPupilFromRegister(pupilRegisterId));

            if (ModelState.IsValid) return BadRequest(ModelState);

            return Ok(pupil);
        }


        /// <summary>
        /// Post request
        /// </summary>
        /// <param name="pupilRegisterCreate"></param>
        /// <returns></returns>
        //Post register
        [HttpPost]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRegisterPupil([FromBody] RegisterCreateDto pupilRegisterCreate)
        {
            if (pupilRegisterCreate == null) return BadRequest(ModelState);

            if (_registerRepository.RegisterPupilExists(pupilRegisterCreate.RegisterPupilId))
            {
                ModelState.AddModelError("", "Pupil already exists on this register.");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var pupilRegisterMap = _mapper.Map<Register>(pupilRegisterCreate);

            if (!_registerRepository.CreateRegisterPupil(pupilRegisterMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");
        }


        /// <summary>
        /// Put request
        /// </summary>
        /// <param name="pupilRegisterId"></param>
        /// <param name="pupilRegisterUpdate"></param>
        /// <returns></returns>
        //Put register
        [HttpPut("{pupilRegisterId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateRegisterPupil(Guid pupilRegisterId, [FromBody] RegisterUpdateDto pupilRegisterUpdate)
        {
            if (pupilRegisterUpdate == null) return BadRequest(ModelState);

            if (pupilRegisterId != pupilRegisterUpdate.RegisterPupilId)
            {
                ModelState.AddModelError("IdMissMatch", "Update Id and Body Id must match.");
                return BadRequest(ModelState);
            }

            if (!_registerRepository.RegisterPupilExists(pupilRegisterId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var pupilRegisterMap = _mapper.Map<Register>(pupilRegisterUpdate);

            var pupilDetails = _registerRepository.GetPupilFromRegister(pupilRegisterMap.RegisterPupilId);

            pupilRegisterMap.RegisterPupilName = pupilDetails.RegisterPupilName;
            pupilRegisterMap.RegisterPupilGender = pupilDetails.RegisterPupilGender;
            pupilRegisterMap.RegisterDateOfBirth = pupilDetails.RegisterDateOfBirth;
            
            if (!_registerRepository.UpdateRegisterPupil(pupilRegisterMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        /// <summary>
        /// Delete request
        /// </summary>
        /// <param name="registerPupilId"></param>
        /// <returns></returns>
        //delete register
        [HttpDelete("{registerPupilId}")]
        //[ServiceFilter(typeof(AuthFilter))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRegisterPupil(Guid registerPupilId)
        {
            if (!_registerRepository.RegisterPupilExists(registerPupilId)) return NotFound();

            var registerPupilToDelete = _registerRepository.GetPupilFromRegister(registerPupilId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_registerRepository.DeleteRegisterPupil(registerPupilToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting the Pupil");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
