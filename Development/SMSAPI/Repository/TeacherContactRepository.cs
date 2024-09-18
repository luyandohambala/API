using SMSAPI.Data;
using SMSAPI.Interfaces;
using SMSAPI.Models;

namespace SMSAPI.Repository
{
    public class TeacherContactRepository : ITeacherContactRepository
    {
        private readonly DataContext _context;

        public TeacherContactRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateTeacherContact(TeacherContact teacherContact)
        {
            _context.Add(teacherContact);

            return Save();
        }

        public bool DeleteTeacherContact(TeacherContact teacherContact)
        {
            _context.Remove(teacherContact);

            return Save();
        }

        public Guid GetTeacherIdByTeacherContactId(Guid teacherContactId)
        {
            return _context.TeacherContacts.Where(tc => tc.TeacherContactId == teacherContactId).Select(tc => tc.TeacherContactTeacherId).FirstOrDefault();
        }

        public TeacherContact GetTeacherContact(Guid teacherContactId)
        {
            return _context.TeacherContacts.Where(tc => tc.TeacherContactId == teacherContactId).FirstOrDefault();
        }

        public ICollection<TeacherContact> GetTeacherContacts()
        {
            return [.. _context.TeacherContacts.OrderBy(tc => tc.TeacherContactId)];
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool TeacherContactExists(Guid teacherContactId)
        {
            return _context.TeacherContacts.Any(tc => tc.TeacherContactId == teacherContactId);
        }

        public bool UpdateTeacherContact(TeacherContact teacherContact)
        {
            _context.Update(teacherContact);

            return Save();
        }
    }
}
