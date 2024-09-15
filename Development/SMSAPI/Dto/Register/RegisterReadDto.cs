using SMSAPI.Models;

namespace SMSAPI.Dto.Register
{
    public class RegisterReadDto
    {
        public Guid RegisterPupilId { get; set; }

        public string RegisterPupilName { get; set; }

        public string RegisterPupilGender { get; set; }

        public DateOnly RegisterDateOfBirth { get; set; }

        public DateOnly DateOfRegister { get; set; }

        public string RegisterAction { get; set; }

    }
}
