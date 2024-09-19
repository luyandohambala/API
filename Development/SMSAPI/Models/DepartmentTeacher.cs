namespace SMSAPI.Models
{
    public class DepartmentTeacher
    {
        public Guid DepartmentId { get; set; }

        public Guid TeacherId { get; set; }

        public Teacher Teacher { get; set; }

        public Department Department { get; set; }  
    }
}
