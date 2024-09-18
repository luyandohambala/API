using SMSAPI.Models;

namespace SMSAPI.Interfaces
{
    public interface ITeacherContactRepository
    {
        //read methods
        ICollection<TeacherContact> GetTeacherContacts();

        Guid GetTeacherIdByTeacherContactId(Guid teacherContactId);

        TeacherContact GetTeacherContact(Guid teacherContactId);


        //write methods
        bool CreateTeacherContact(TeacherContact teacherContact);

        bool UpdateTeacherContact(TeacherContact teacherContact);

        bool Save();


        //delete methods
        bool DeleteTeacherContact(TeacherContact teacherContact);   


        //validation methods
        bool TeacherContactExists(Guid teacherContactId);
    }
}
