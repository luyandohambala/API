namespace SMSAPI.Models
{
    public class Register
    {
        public Guid RegisterPupilId { get; set; }

        public string RegisterPupilName { get; set; }

        public string RegisterPupilGender { get; set; }
        
        public DateOnly RegisterDateOfBirth { get; set; }

        public DateOnly DateOfRegister { get; set; }

        public string RegisterAction { get; set; }


        //Navigation properties
        public ICollection<ClassRoomRegister> ClassRegisters { get; set; }  

    }
}
