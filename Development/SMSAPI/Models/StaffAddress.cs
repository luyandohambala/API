namespace SMSAPI.Models
{
    public class StaffAddress
    {
        public Guid StaffAddressId { get; set; }

        public string Area { get; set; }

        public string Street { get; set; }

        public string HouseNo { get; set; }

        public Guid StaffAddressStaffId { get; set; }


        //navigation properties
        public Staff Staff { get; set; }

    }
}
