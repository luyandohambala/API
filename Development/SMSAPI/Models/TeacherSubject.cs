namespace SMSAPI.Models
{
    public class TeacherSubject
    {
        public Guid TeacherId { get; set; }

        public Guid SubjectId { get; set; }

        public Teacher Teacher { get; set; }

        public Subject Subject { get; set; }
    }
}
