using SMSAPI.Models;

namespace SMSAPI.Dto.TeacherAddressDto
{
    public class TeacherAddressUpdateDto
    {
        public Guid TeacherAddressId { get; set; }

        public string Area { get; set; }

        public string Street { get; set; }

        public string HouseNo { get; set; }
    }
}
