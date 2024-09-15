namespace SMSAPI.Models
{
    public class GuardianContact
    {

        public Guid GuardianContactId { get; set; }

        public string GuardianContactOnePhone { get; set; }

        public string GuardianContactOneEmail { get; set; }

        public string GuardianContactTwoPhone { get; set; }

        public string GuardianContactTwoEmail { get; set; }

        public Guid GuardianContactGuardianId { get; set; }


        //Navigation properties
        public Guardian Guardian { get; set; }
    }
}
