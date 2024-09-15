using SMSAPI.Models;

namespace SMSAPI.Interfaces
{
    public interface IGuardianContactRepository
    {
        //read methods
        ICollection<GuardianContact> GetGuardianContacts();

        Guid GetGuardianIdByGuardianContactId(Guid guardianContactId);

        GuardianContact GetGuardianContact(Guid guardianContactId);

        
        //write methods
        bool CreateGuardianContact(GuardianContact guardianContact);

        bool UpdateGuardianContact(GuardianContact guardianContact);

        bool Save();


        //delete methods
        bool DeleteGuardianContact(GuardianContact guardianContact);

        
        //validation methods
        bool GuardianContactExists(Guid guardianContactId);
    }
}
