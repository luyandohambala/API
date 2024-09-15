using SMSAPI.Models;

namespace SMSAPI.Dto.ClassRegisterDto
{
    public class ClassRegisterUpdateDto
    {
        public Guid ClassRegisterId { get; set; }

        public string ClassRegisterTerm { get; set; }

        public DateOnly ClassRegisterDate { get; set; }

        public string ClassRegisterAction { get; set; }
    }
}
