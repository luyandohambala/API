using SMSAPI.Models;

namespace SMSAPI.Dto.ReportCardDto
{
    public class ReportCardReadDto
    {
        public Guid ReportCardId { get; set; }

        public string ReportCardPupilName { get; set; }

        public string ReportCardTeacherName { get; set; }

        public DateOnly ReportCardSubmissionDate { get; set; }

        public string ReportCardSchoolTerm { get; set; }

        public string ReportCardStatus { get; set; }
    }
}
