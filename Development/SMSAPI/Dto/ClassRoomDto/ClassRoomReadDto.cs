using SMSAPI.Models;

namespace SMSAPI.Dto.ClassRoomDto
{
    public class ClassRoomReadDto
    {
        public Guid ClassId { get; set; }

        public string ClassName { get; set; }

        public int PupilTotal { get; set; }

    }
}
