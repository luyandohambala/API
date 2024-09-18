namespace SMSAPI.Models
{
    public class TeacherContact
    {
        public Guid TeacherContactId { get; set; }

        public string TeacherContactEmail { get; set; }

        public string TeacherContactPhoneOne { get; set; }

        public string TeacherContactPhoneTwo { get; set; }

        public Guid TeacherContactTeacherId { get; set; }


        //Navigation properties
        public Teacher Teacher { get; set; }

    }
}
