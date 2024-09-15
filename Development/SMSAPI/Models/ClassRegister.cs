namespace SMSAPI.Models
{
    public class ClassRegister
    {
        public Guid ClassRegisterId { get; set; }

        public string ClassRegisterName { get; set; }

        public string ClassRegisterGrade { get; set; }

        public string ClassRegisterTerm { get; set; }

        public DateOnly ClassRegisterDate { get; set; }

        public string ClassRegisterAction { get; set; }


        //Navigation properties 
        public ICollection<ClassRoomRegister> Registers { get; set; }
    }
}
