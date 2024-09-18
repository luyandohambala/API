
 namespace SMSAPI.Models
{
    public class Subject
    {
        public Guid SubjectId { get; set; }

        public string SubjectName { get; set; }

        public Guid SubjectGradeId { get; set; }

        public ICollection<ClassRoomSubject> ClassRooms { get; set; }

        public ICollection<TeacherSubject> Teachers { get; set; }   
    }
}
