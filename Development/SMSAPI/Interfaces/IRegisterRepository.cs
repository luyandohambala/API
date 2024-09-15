using SMSAPI.Models;

namespace SMSAPI.Interfaces
{
    public interface IRegisterRepository
    {
        //read methods 
        ICollection<Register> GetPupilsFromRegister(); 

        ICollection<Register> GetRegisterByClassId(Guid classRoomRegisterId);

        Register GetPupilFromRegister(Guid pupilRegisterId);


        //write methods
        bool CreateRegisterPupil(Register pupilRegister);

        bool UpdateRegisterPupil(Register pupilRegister);

        bool Save();


        //delete methods
        bool DeleteRegisterPupil(Register pupilRegister);


        //validation method
        bool RegisterPupilExists(Guid pupilRegisterId);
    }
}
