using Microsoft.AspNetCore.Mvc;
using SMSAPI.Data;
using SMSAPI.Interfaces;
using SMSAPI.Models;

namespace SMSAPI.Repository
{
    public class GuardianContactRepository : IGuardianContactRepository
    {
        private readonly DataContext _context;

        public GuardianContactRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateGuardianContact(GuardianContact guardianContact)
        {
            _context.Add(guardianContact);

            return Save();
        }

        public bool DeleteGuardianContact(GuardianContact guardianContact)
        {
            _context.Remove(guardianContact);

            return Save();
        }

        public GuardianContact GetGuardianContact(Guid guardianContactId)
        {
            return _context.GuardianContacts.Where(gc => gc.GuardianContactId == guardianContactId).FirstOrDefault();
        }

        public ICollection<GuardianContact> GetGuardianContacts()
        {
            return [.. _context.GuardianContacts.OrderBy(gc => gc.GuardianContactId)];
        }

        public Guid GetGuardianIdByGuardianContactId(Guid guardianContactId)
        {
            return _context.GuardianContacts.Where(gc => gc.GuardianContactId == guardianContactId).Select(g => g.GuardianContactGuardianId).FirstOrDefault();
        }

        public bool GuardianContactExists(Guid guardianContactId)
        {
            return _context.GuardianContacts.Any(gc => gc.GuardianContactId == guardianContactId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool UpdateGuardianContact(GuardianContact guardianContact)
        {
            _context.Update(guardianContact);

            return Save();
        }
    }
}
