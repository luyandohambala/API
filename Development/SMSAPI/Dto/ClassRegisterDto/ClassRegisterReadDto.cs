using SMSAPI.Models;

namespace SMSAPI.Dto.ClassRegisterDto
{
    public class ClassRegisterReadDto
    {
        public Guid ClassRegisterId { get; set; }

        public string ClassRegisterName { get; set; }

        public string ClassRegisterGrade { get; set; }

        public string ClassRegisterTerm { get; set; }

        public DateOnly ClassRegisterDate { get; set; }

        public string ClassRegisterAction { get; set; }
    }
}
