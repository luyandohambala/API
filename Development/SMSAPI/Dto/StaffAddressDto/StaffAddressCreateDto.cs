using SMSAPI.Models;

namespace SMSAPI.Dto.StaffAddressDto
{
    public class StaffAddressCreateDto
    {
        public Guid StaffAddressId { get; set; }

        public string Area { get; set; }

        public string Street { get; set; }

        public string HouseNo { get; set; }

        public Guid StaffAddressStaffId { get; set; }
    }
}
