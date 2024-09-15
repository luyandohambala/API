using SMSAPI.Models;

namespace SMSAPI.Dto.ClassRoomDto
{
    public class ClassRoomWithSubjectsDto
    {
        public Guid ClassId { get; set; }

        public string ClassName { get; set; }

        public int PupilTotal { get; set; }

        public ICollection<Subject> Subjects{ get; set; }
    }
}
