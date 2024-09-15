using Microsoft.EntityFrameworkCore;
using SMSAPI.Data;
using SMSAPI.Interfaces;
using SMSAPI.Models;

namespace SMSAPI.Repository
{
    public class ReportCardRepository : IReportCardRepository
    {
        private readonly DataContext _context;

        public ReportCardRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateReportCard(ReportCard reportCard)
        {
            _context.Add(reportCard);

            return Save();
        }

        public bool DeleteReportCard(ReportCard reportCard)
        {
            _context.Remove(reportCard);

            return Save();
        }

        public bool DeleteReportCards(List<ReportCard> reportCards)
        {
            _context.RemoveRange(reportCards);

            return Save();
        }

        public Guid GetClassRoomIdByReportCardId(Guid reportCardId)
        {
            return _context.ReportCards.Where(rc => rc.ReportCardId == reportCardId).Select(cr => cr.ReportCardClassId).FirstOrDefault();

        }

        public Guid GetPupilIdByReportCardId(Guid reportCardId)
        {
            return _context.ReportCards.Where(rc => rc.ReportCardId == reportCardId).Select(p => p.ReportCardPupilId).FirstOrDefault();
        }

        public ReportCard GetReportCard(Guid reportCardId)
        {
            return _context.ReportCards.Where(rc => rc.ReportCardId == reportCardId).Include(rcs => rcs.ReportCardSubjects).FirstOrDefault();
        }

        public ICollection<ReportCard> GetReportCards()
        {
            return [.. _context.ReportCards.OrderBy(rc => rc.ReportCardId)];
        }

        public ICollection<ReportCardSubjects> GetReportCardSubjectsByReportCardId(Guid reportCardId)
        {
            return [.. _context.ReportCardReportCardSubjects.Where(rc => rc.ReportCardId == reportCardId).Select(rcs => rcs.ReportCardSubject)];
        }

        public bool ReportCardExists(Guid reportCardId)
        {
            return _context.ReportCards.Any(rc => rc.ReportCardId == reportCardId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool UpdateReportCard(ReportCard reportCard)
        {
            _context.Update(reportCard);

            return Save();
        }
    }
}
