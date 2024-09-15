namespace SMSAPI.Models
{
    public class ReportCardSubjects
    {

        public Guid ReportCardSubjectId { get; set; }

        public string ReportCardSubjectName { get; set; }

        public string ReportCardSubjectTeacher { get; set; }

        public int ReportCardSubjectMarks { get; set; }

        public string ReportCardSubjectResult { get; set; } 


        //navigation property
        public ICollection<ReportCardReportCardSubject> ReportCards { get; set; }
    }
}
