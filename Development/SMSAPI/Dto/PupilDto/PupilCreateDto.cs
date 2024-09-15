using SMSAPI.Models;

namespace SMSAPI.Dto.PupilDto
{
    public class PupilCreateDto
    {
        public Guid PupilId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public Guid PupilClassId { get; set; }

        public Guid PupilGradeId { get; set; }

        public Guid PupilGuardianId { get; set; }
    }
}
