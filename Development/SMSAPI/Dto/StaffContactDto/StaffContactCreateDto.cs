namespace SMSAPI.Dto.StaffContactDto
{
    public class StaffContactCreateDto
    {
        public Guid StaffContactId { get; set; }

        public string StaffContactEmail { get; set; }

        public string StaffContactPhoneOne { get; set; }

        public string StaffContactPhoneTwo { get; set; }

        public Guid StaffContactStaffId { get; set; }
    }
}
