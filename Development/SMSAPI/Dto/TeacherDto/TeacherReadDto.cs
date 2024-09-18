using SMSAPI.Models;

namespace SMSAPI.Dto.TeacherDto
{
    public class TeacherReadDto
    {
        public Guid TeacherId { get; set; }

        public string TeacherTitle { get; set; }

        public string TeacherFirstName { get; set; }

        public string TeacherLastName { get; set; }

        public string TeacherDepartmentId { get; set; }

        public string TeacherGrade { get; set; }
    }
}
