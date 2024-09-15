using SMSAPI.Models;

namespace SMSAPI.Dto.PupilDto
{
    public class PupilReadDto
    {
        public Guid PupilId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateOnly DateOfBirth { get; set; }

    }
}
