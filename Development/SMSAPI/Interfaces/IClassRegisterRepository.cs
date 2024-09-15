using SMSAPI.Models;

namespace SMSAPI.Interfaces
{
    public interface IClassRegisterRepository
    {

        //read methods
        ICollection<ClassRegister> GetClassRegisters();

        ClassRegister GetClassRegisterByClassId(Guid classRoomRegisterId);


        //write methods
        bool CreateClassRegister(ClassRegister classRoomRegister);

        bool UpdateClassRegister(ClassRegister classRoomRegister);

        bool Save();


        //delete methods
        bool DeleteClassRegister(ClassRegister classRoomRegister);


        //validation methods
        bool ClassRoomRegisterExists(Guid classRoomRegisterId); 
    }
}
