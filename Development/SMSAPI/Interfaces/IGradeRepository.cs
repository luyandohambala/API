using SMSAPI.Models;

namespace SMSAPI.Interfaces
{
    public interface IGradeRepository
    {
        //read methods
        ICollection<Grade> GetGrades();

        ICollection<ClassRoom> GetClassRoomsByGradeId(Guid gradeId);

        Grade GetGrade(Guid gradeId);

        
        //write methods
        bool CreateGrade(Grade grade);

        bool UpdateGrade(Grade grade);

        bool Save();


        //delete methods
        bool DeleteGrade(Grade gradeId); 


        //validation methods
        bool GradeExists(Guid gradeId); 
    }
}
