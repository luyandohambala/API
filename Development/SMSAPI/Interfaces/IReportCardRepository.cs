using SMSAPI.Models;

namespace SMSAPI.Interfaces
{
    public interface IReportCardRepository
    {
        //read methods
        ICollection<ReportCard> GetReportCards();

        ICollection<ReportCardSubjects> GetReportCardSubjectsByReportCardId(Guid reportCardId);

        ReportCard GetReportCard(Guid reportCardId);

        Guid GetClassRoomIdByReportCardId(Guid reportCardId);

        Guid GetPupilIdByReportCardId(Guid reportCardId);


        //write methods
        bool CreateReportCard(ReportCard reportCard);

        bool UpdateReportCard(ReportCard reportCard);

        bool Save();


        //Delete methods
        bool DeleteReportCard(ReportCard reportCard);

        bool DeleteReportCards(List<ReportCard> reportCards);


        //validation methods
        bool ReportCardExists(Guid reportCardId);
    }
}
