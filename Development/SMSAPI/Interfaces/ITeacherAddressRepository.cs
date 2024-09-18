using SMSAPI.Models;

namespace SMSAPI.Interfaces
{
    public interface ITeacherAddressRepository
    {
        //read methods
        ICollection<TeacherAddress> GetTeacherAddresses();

        Guid GetTeacherIdByTeacherAddressId(Guid teacherAddressId);

        TeacherAddress GetTeacherAddress(Guid teacherAddressId);


        //write methods
        bool CreateTeacherAddress(TeacherAddress teacherAddress);

        bool UpdateTeacherAddress(TeacherAddress teacherAddress);

        bool Save();


        //delete methods
        bool DeleteTeacherAddress(TeacherAddress teacherAddress);


        //validation methods
        bool TeacherAddressExists(Guid teacherAddressId);   
    }
}
