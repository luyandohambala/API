using SMSAPI.Models;

namespace SMSAPI.Interfaces
{
    public interface IReportCardSubjectRepository
    {
        //read methods
        ICollection<ReportCardSubjects> GetReportCardSubjects();

        ReportCardSubjects GetReportCardSubject(Guid reportCardSubjectId);

        ICollection<ReportCard> GetReportCardIdByReportCardSubjectId(Guid reportCardSubjectId);

        
        //write methods
        bool CreateReportCardSubject(ReportCardSubjects reportCardSubject);

        bool UpdateReportCardSubject(ReportCardSubjects reportCardSubjects);

        bool Save();


        //delete methods
        bool DeleteReportCardSubject(ReportCardSubjects reportCardSubjects);


        //validation methods
        bool ReportCardSubjectExists(Guid reportCardSubjectId); 
    }
}
