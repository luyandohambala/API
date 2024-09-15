namespace SMSAPI.Models
{
    public class ReportCard
    {
        public Guid ReportCardId { get; set; }

        public string ReportCardPupilName { get; set; }

        public string ReportCardTeacherName { get; set; }

        public Guid ReportCardClassId { get; set; }

        public DateOnly ReportCardSubmissionDate { get; set; }

        public string ReportCardSchoolTerm { get; set; }  

        public string ReportCardStatus { get; set; }    

        public Guid ReportCardPupilId { get; set; } 

        //navigation property
        public Pupil Pupil { get; set; }

        public ClassRoom ClassRoom { get; set; }

        public ICollection<ReportCardReportCardSubject> ReportCardSubjects { get; set; }
    }
}
