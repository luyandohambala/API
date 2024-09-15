namespace SMSAPI.Models
{
    public class ClassRoomRegister
    {
        public Guid ClassRoomId { get; set; }

        public Guid RegisterId { get; set; }


        //Navigation properties
        public ClassRegister ClassRoom { get; set; }

        public Register Register { get; set; }
    }
}
