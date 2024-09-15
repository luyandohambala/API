namespace SMSAPI.Dto.ClassRoomDto
{
    public class ClassRoomCreateDto
    {
        public Guid ClassId { get; set; }

        public string ClassName { get; set; }

        public Guid ClassGradeId { get; set; }

        public int PupilTotal { get; set; }
    }
}
