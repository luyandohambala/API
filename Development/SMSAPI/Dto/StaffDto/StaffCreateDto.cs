using SMSAPI.Models;

namespace SMSAPI.Dto.StaffDto
{
    public class StaffCreateDto
    {
        public Guid StaffId { get; set; }

        public string StaffTitle { get; set; }

        public string StaffFirstName { get; set; }

        public string StaffLastName { get; set; }

        public string StaffDepartmentId { get; set; }
    }
}
