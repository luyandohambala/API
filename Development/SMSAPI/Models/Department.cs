namespace SMSAPI.Models
{
    public class Department
    {
        public Guid DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentRole { get; set; }  



        //navigation properties
        public ICollection<DepartmentTeacher> Teachers { get; set; }
         
        public ICollection<Staff> Staff { get; set; }   
    }
}
