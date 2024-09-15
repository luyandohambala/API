using SMSAPI.Models;

namespace SMSAPI.Interfaces
{
    public interface IGuardianRepository
    {
        //read methods
        ICollection<Guardian> GetGuardians();

        ICollection<Pupil> GetPupilsByGuardianId(Guid guardianId);

        GuardianContact GetGuardianContactByGuardianId(Guid guardianId);

        Guardian GetGuardian(Guid guardianId);

        
        //write methods
        bool CreateGuardian(Guardian guardian);

        bool UpdateGuardian(Guardian guardian);

        bool Save();


        //delete methods
        bool DeleteGuardian(Guardian guardianId);


        //validate methods
        bool GuardianExists(Guid guardianId);
    }
}
