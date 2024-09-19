namespace SMSAPI.Models
{
    public class Staff
    {

        public Guid StaffId { get; set; }

        public string StaffTitle { get; set; }  

        public string StaffFirstName { get; set; }

        public string StaffLastName { get; set; }


        public Guid StaffDepartmentId { get; set; } 

        //navigation properties
        public Department Department { get; set; }

        public StaffAddress StaffAddress { get; set; }

        public StaffContact StaffContact { get; set; }  
    }
}
