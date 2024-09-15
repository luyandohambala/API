using SMSAPI.Models;

namespace SMSAPI.Dto.ReportCardSubjectDto
{
    public class ReportCardSubjectCreateDto
    {
        public Guid ReportCardSubjectId { get; set; }

        public string ReportCardSubjectName { get; set; }

        public string ReportCardSubjectTeacher { get; set; }

        public int ReportCardSubjectMarks { get; set; }

        public string ReportCardSubjectResult { get; set; }
    }
}
