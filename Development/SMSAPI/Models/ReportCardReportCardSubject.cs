namespace SMSAPI.Models
{
    public class ReportCardReportCardSubject
    {
        public Guid ReportCardId { get; set; }

        public Guid ReportCardSubjectId { get; set; }

        public ReportCard ReportCard { get; set; }

        public ReportCardSubjects ReportCardSubject { get; set; }
    }
}
