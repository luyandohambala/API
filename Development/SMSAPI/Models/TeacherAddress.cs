namespace SMSAPI.Models
{
    public class TeacherAddress
    {
        public Guid TeacherAddressId { get; set; }

        public string Area { get; set; }

        public string Street { get; set; }

        public string HouseNo { get; set; }
            
        public Guid TeacherAddressTeacherId { get; set; }

        //navigation properties
        public Teacher Teacher { get; set; }
    }
}
