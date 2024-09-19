using SMSAPI.Data;
using SMSAPI.Models;

namespace SMSAPI.Repository
{
    public class StaffAddressRepository
    {
        private readonly DataContext _context;

        public StaffAddressRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateStaffAddress(StaffAddress staffAddress)
        {
            _context.Add(staffAddress);

            return Save();
        }

        public bool DeleteStaffAddress(StaffAddress staffAddress)
        {
            _context.Remove(staffAddress);

            return Save();
        }

        public StaffAddress GetStaffAddress(Guid staffAddressId)
        {
            return _context.StaffAddresses.Where(ta => ta.StaffAddressId == staffAddressId).FirstOrDefault();
        }

        public ICollection<StaffAddress> GetStaffAddresses()
        {
            return [.. _context.StaffAddresses.OrderBy(ta => ta.StaffAddressId)];
        }

        public Guid GetStaffIdByStaffAddressId(Guid staffAddressId)
        {
            return _context.StaffAddresses.Where(ta => ta.StaffAddressId == staffAddressId).Select(ta => ta.StaffAddressStaffId).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool StaffAddressExists(Guid staffAddressId)
        {
            return _context.StaffAddresses.Any(ta => ta.StaffAddressId == staffAddressId);
        }

        public bool UpdateStaffAddress(StaffAddress staffAddress)
        {
            _context.Update(staffAddress);

            return Save();
        }
    }
}
