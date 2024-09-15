using SMSAPI.Models;

namespace SMSAPI.Interfaces
{
    public interface ISubjectRepository
    {
        //read methods
        ICollection<Subject> GetSubjects();

        ICollection<ClassRoom> GetClassRoomsBySubjectId(Guid subjectId);

        Subject GetSubject(Guid subjectId);

        
        //write methods
        bool CreateSubject(Subject subject);

        bool UpdateSubject(Subject subject);

        bool Save();


        //delete methods
        bool DeleteSubject(Subject subjectId);


        //validation methods
        bool SubjectExists(Guid subject);
    }
}
