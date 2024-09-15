using SMSAPI.Models;

namespace SMSAPI.Dto.Register
{
    public class RegisterUpdateDto
    {
        public Guid RegisterPupilId { get; set; }

        public DateOnly DateOfRegister { get; set; }

        public string RegisterAction { get; set; }
    }
}
