using SMSAPI.Data;
using SMSAPI.Models;

namespace SMSAPI.Repository
{
    public class StaffContactRepository
    {
        private readonly DataContext _context;

        public StaffContactRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateStaffContact(StaffContact staffContact)
        {
            _context.Add(staffContact);

            return Save();
        }

        public bool DeleteStaffContact(StaffContact staffContact)
        {
            _context.Remove(staffContact);

            return Save();
        }

        public Guid GetStaffIdByStaffContactId(Guid staffContactId)
        {
            return _context.StaffContacts.Where(tc => tc.StaffContactId == staffContactId).Select(tc => tc.StaffContactStaffId).FirstOrDefault();
        }

        public StaffContact GetStaffContact(Guid staffContactId)
        {
            return _context.StaffContacts.Where(tc => tc.StaffContactId == staffContactId).FirstOrDefault();
        }

        public ICollection<StaffContact> GetStaffContacts()
        {
            return [.. _context.StaffContacts.OrderBy(tc => tc.StaffContactId)];
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool StaffContactExists(Guid staffContactId)
        {
            return _context.StaffContacts.Any(tc => tc.StaffContactId == staffContactId);
        }

        public bool UpdateStaffContact(StaffContact staffContact)
        {
            _context.Update(staffContact);

            return Save();
        }
    }
}
