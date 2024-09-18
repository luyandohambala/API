namespace SMSAPI.Models
{
    public class TeacherGrades
    {
        public Guid TeacherId { get; set; }

        public Guid GradeId { get; set; }

        public Teacher Teacher { get; set; }

        public Grade Grade { get; set; }    
    }
}
