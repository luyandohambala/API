using SMSAPI.Data;
using SMSAPI.Interfaces;
using SMSAPI.Models;

namespace SMSAPI.Repository
{
    public class TeacherAddressRepository : ITeacherAddressRepository
    {
        private readonly DataContext _context;

        public TeacherAddressRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateTeacherAddress(TeacherAddress teacherAddress)
        {
            _context.Add(teacherAddress);

            return Save();
        }

        public bool DeleteTeacherAddress(TeacherAddress teacherAddress)
        {
            _context.Remove(teacherAddress);

            return Save();
        }

        public TeacherAddress GetTeacherAddress(Guid teacherAddressId)
        {
            return _context.TeacherAddresses.Where(ta => ta.TeacherAddressId == teacherAddressId).FirstOrDefault();
        }

        public ICollection<TeacherAddress> GetTeacherAddresses()
        {
            return [.. _context.TeacherAddresses.OrderBy(ta => ta.TeacherAddressId)];
        }

        public Guid GetTeacherIdByTeacherAddressId(Guid teacherAddressId)
        {
            return _context.TeacherAddresses.Where(ta => ta.TeacherAddressId == teacherAddressId).Select(ta => ta.TeacherAddressTeacherId).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool TeacherAddressExists(Guid teacherAddressId)
        {
            return _context.TeacherAddresses.Any(ta => ta.TeacherAddressId == teacherAddressId);
        }

        public bool UpdateTeacherAddress(TeacherAddress teacherAddress)
        {
            _context.Update(teacherAddress);

            return Save();
        }
    }
}
