namespace SMSAPI.Dto.TeacherContactDto
{
    public class TeacherContactUpdateDto
    {
        public Guid TeacherContactId { get; set; }

        public string TeacherContactEmail { get; set; }

        public string TeacherContactPhoneOne { get; set; }

        public string TeacherContactPhoneTwo { get; set; }
    }
}
