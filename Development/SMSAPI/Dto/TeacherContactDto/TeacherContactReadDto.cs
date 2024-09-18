namespace SMSAPI.Dto.TeacherContactDto
{
    public class TeacherContactReadDto
    {
        public Guid TeacherContactId { get; set; }

        public string TeacherContactEmail { get; set; }

        public string TeacherContactPhoneOne { get; set; }

        public string TeacherContactPhoneTwo { get; set; }

        public Guid TeacherContactTeacherId { get; set; }

    }
}
