using SMSAPI.Data;
using SMSAPI.Interfaces;
using SMSAPI.Models;

namespace SMSAPI.Repository
{
    public class ReportCardSubjectRepository : IReportCardSubjectRepository
    {
        private readonly DataContext _context;

        public ReportCardSubjectRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateReportCardSubject(ReportCardSubjects reportCardSubject)
        {
            _context.Add(reportCardSubject);

            return Save();
        }

        public bool DeleteReportCardSubject(ReportCardSubjects reportCardSubjects)
        {
            _context.Remove(reportCardSubjects);

            return Save();
        }

        public ICollection<ReportCard> GetReportCardIdByReportCardSubjectId(Guid reportCardSubjectId)
        {
            return [.. _context.ReportCardReportCardSubjects.Where(rcs => rcs.ReportCardSubjectId == reportCardSubjectId).Select(rc => rc.ReportCard)];
        }

        public ReportCardSubjects GetReportCardSubject(Guid reportCardSubjectId)
        {
            return _context.ReportCardSubjects.Where(rcs => rcs.ReportCardSubjectId == reportCardSubjectId).FirstOrDefault();
        }

        public ICollection<ReportCardSubjects> GetReportCardSubjects()
        {
            return [.. _context.ReportCardSubjects.OrderBy(rcs => rcs.ReportCardSubjectId)];
        }

        public bool ReportCardSubjectExists(Guid reportCardSubjectId)
        {
            return _context.ReportCardSubjects.Any(rcs => rcs.ReportCardSubjectId == reportCardSubjectId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool UpdateReportCardSubject(ReportCardSubjects reportCardSubjects)
        {
            _context.Update(reportCardSubjects);

            return Save();
        }
    }
}
