using SMSAPI.Data;
using SMSAPI.Interfaces;
using SMSAPI.Models;

namespace SMSAPI.Repository
{
    public class StaffRepository : IStaffRepository
    {
        private readonly DataContext _context;

        public StaffRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateStaff(Staff staff)
        {
            _context.Add(staff);

            return Save();
        }

        public bool DeleteStaff(Staff staff)
        {
            _context.Remove(staff);

            return Save();
        }

        public Staff GetStaffById(Guid staffId)
        {
            return _context.Staff.Where(t => t.StaffId == staffId).FirstOrDefault();
        }

        public ICollection<Staff> GetStaff()
        {
            return [.. _context.Staff.OrderBy(t => t.StaffId)];
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool StaffExists(Guid staffId)
        {
            return _context.Staff.Any(t => t.StaffId == staffId);
        }

        public bool UpdateStaff(Staff staff)
        {
            _context.Update(staff);

            return Save();
        }
    }
}
