namespace SMSAPI.Models
{
    public class Guardian
    {
        public Guid GuardianId { get; set; }

        public string TitleOne { get; set; }

        public string GuardianOneName { get; set; }
        virtual 
        public string TitleTwo { get; set; }

        public string GuardianTwoName { get; set; }


        //Navigation properties
        public GuardianContact Contact { get; set; }

        public ICollection<Pupil> Pupils { get; set; }
    }
}
