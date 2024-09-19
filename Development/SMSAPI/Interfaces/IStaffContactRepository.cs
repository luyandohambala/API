using SMSAPI.Models;

namespace SMSAPI.Interfaces
{
    public interface IStaffContactRepository
    {
        //read methods
        ICollection<StaffContact> GetStaffContacts();

        Guid GetStaffIdByStaffContactId(Guid staffContactId);

        StaffContact GetStaffContact(Guid staffContactId);


        //write methods
        bool CreateStaffContact(StaffContact staffContact);

        bool UpdateStaffContact(StaffContact staffContact);

        bool Save();


        //delete methods
        bool DeleteStaffContact(StaffContact staffContact);


        //validation methods
        bool StaffContactExists(Guid staffContactId);
    }
}
