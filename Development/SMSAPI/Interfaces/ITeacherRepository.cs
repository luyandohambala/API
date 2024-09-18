using SMSAPI.Models;

namespace SMSAPI.Interfaces
{
    public interface ITeacherRepository
    {
        //read methods
        ICollection<Teacher> GetTeachers();

        ICollection<Subject> GetSubjectsByTeacherId(Guid teacherId);

        Teacher GetTeacher(Guid teacherId);


        //write methods
        bool CreateTeacher(Teacher teacher);

        bool UpdateTeacher(Teacher teacher);

        bool Save();


        //delete methods
        bool DeleteTeacher(Teacher teacher);


        //validation methods
        bool TeacherExists(Guid teacherId); 
    }
}
