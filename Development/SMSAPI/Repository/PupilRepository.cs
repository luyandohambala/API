using Microsoft.EntityFrameworkCore;
using SMSAPI.Data;
using SMSAPI.Interfaces;
using SMSAPI.Models;

namespace SMSAPI.Repository
{
    public class PupilRepository : IPupilRepository
    {
        private readonly DataContext _context;

        public PupilRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreatePupil(Pupil pupil)
        {
            _context.Add(pupil);

            return Save();
        }

        public bool DeletePupil(Pupil pupil)
        {
            _context.Remove(pupil);

            return Save();
        }

        public bool DeletePupils(List<Pupil> pupils)
        {
            _context.RemoveRange(pupils);

            return Save();
        }

        public Guid GetClassIdByPupilId(Guid pupilId)
        {
            return _context.Pupils.Where(p => p.PupilId == pupilId).Select(cr => cr.PupilClassId).FirstOrDefault();
        }

        public Guid GetGradeIdByPupilId(Guid pupilId)
        {
            return _context.Pupils.Where(p => p.PupilId == pupilId).Select(gr => gr.PupilGradeId).FirstOrDefault();
        }

        public Guid GetGuardianIdByPupilId(Guid pupilId)
        {
            return _context.Pupils.Where(p => p.PupilId == pupilId).Select(gd => gd.PupilGuardianId).FirstOrDefault();
        }

        public Pupil GetPupil(Guid pupilId)
        {
            return _context.Pupils.Where(p => p.PupilId == pupilId).Include(rc => rc.ReportCards).FirstOrDefault();
        }

        public ICollection<Pupil> GetPupils()
        {
            return [.. _context.Pupils.OrderBy(p => p.PupilId)];
        }

        public ICollection<ReportCard> GetReportCardByPupilId(Guid pupilId)
        {
            return [.. _context.ReportCards.Where(p => p.ReportCardPupilId == pupilId)];
        }

        public bool PupilExists(Guid pupilId)
        {
            return _context.Pupils.Any(p => p.PupilId == pupilId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool UpdatePupil(Pupil pupil)
        {
            _context.Update(pupil);

            return Save();
        }
    }
}
