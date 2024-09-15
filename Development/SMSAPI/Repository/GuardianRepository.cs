using SMSAPI.Data;
using SMSAPI.Interfaces;
using SMSAPI.Models;

namespace SMSAPI.Repository
{
    public class GuardianRepository : IGuardianRepository
    {
        private readonly DataContext _context;

        public GuardianRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateGuardian(Guardian guardian)
        {
            _context.Add(guardian);

            return Save();
        }

        public bool DeleteGuardian(Guardian guardianId)
        {
            _context.Remove(guardianId);

            return Save();
        }

        public Guardian GetGuardian(Guid guardianId)
        {
            return _context.Guardians.Where(g => g.GuardianId == guardianId).FirstOrDefault();
        }

        public GuardianContact GetGuardianContactByGuardianId(Guid guardianId)
        {
            return _context.GuardianContacts.Where(g => g.GuardianContactGuardianId == guardianId).FirstOrDefault();
        }

        public ICollection<Guardian> GetGuardians()
        {
            return [.. _context.Guardians.OrderBy(g => g.GuardianId)];
        }

        public ICollection<Pupil> GetPupilsByGuardianId(Guid guardianId)
        {
            return [.. _context.Pupils.Where(g => g.PupilGuardianId == guardianId)];
        }

        public bool GuardianExists(Guid guardianId)
        {
            return _context.Guardians.Any(g => g.GuardianId == guardianId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool UpdateGuardian(Guardian guardian)
        {
            _context.Update(guardian);

            return Save();
        }
    }
}
