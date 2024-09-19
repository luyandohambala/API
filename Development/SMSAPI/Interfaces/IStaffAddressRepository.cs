using SMSAPI.Models;

namespace SMSAPI.Interfaces
{
    public interface IStaffAddressRepository
    {
        //read methods
        ICollection<StaffAddress> GetStaffAddresses();

        Guid GetStaffIdByStaffAddressId(Guid staffAddressId);

        StaffAddress GetStaffAddress(Guid staffAddressId);


        //write methods
        bool CreateStaffAddress(StaffAddress staffAddress);

        bool UpdateStaffAddress(StaffAddress staffAddress);

        bool Save();


        //delete methods
        bool DeleteStaffAddress(StaffAddress staffAddress);


        //validation methods
        bool StaffAddressExists(Guid staffAddressId);
    }
}
