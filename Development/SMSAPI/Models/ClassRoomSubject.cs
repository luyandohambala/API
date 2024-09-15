namespace SMSAPI.Models
{
    public class ClassRoomSubject
    {
        public Guid ClassRoomId { get; set; }

        public Guid SubjectId { get; set; }

        public ClassRoom ClassRoom { get; set; }

        public Subject Subject { get; set; }
    }
}
