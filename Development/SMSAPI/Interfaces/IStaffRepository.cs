using SMSAPI.Models;

namespace SMSAPI.Interfaces
{
    public interface IStaffRepository
    {
        //read methods
        ICollection<Staff> GetStaff();

        Staff GetStaffById(Guid staffId);


        //write methods
        bool CreateStaff(Staff staff);

        bool UpdateStaff(Staff staff);

        bool Save();


        //delete methods
        bool DeleteStaff(Staff staff);


        //validation methods
        bool StaffExists(Guid staffId);
    }
}
