using SMSAPI.Data;
using SMSAPI.Interfaces;
using SMSAPI.Models;

namespace SMSAPI.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly DataContext _context;

        public TeacherRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateTeacher(Teacher teacher)
        {
            _context.Add(teacher);

            return Save();
        }

        public bool DeleteTeacher(Teacher teacher)
        {
            _context.Remove(teacher);

            return Save();
        }

        public ICollection<Subject> GetSubjectsByTeacherId(Guid teacherId)
        {
            return [.. _context.TeacherSubjects.Where(t => t.TeacherId == teacherId).Select(s => s.Subject)];
        }

        public Teacher GetTeacher(Guid teacherId)
        {
            return _context.Teachers.Where(t => t.TeacherId == teacherId).FirstOrDefault();
        }

        public ICollection<Teacher> GetTeachers()
        {
            return [.. _context.Teachers.OrderBy(t => t.TeacherId)];
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool TeacherExists(Guid teacherId)
        {
            return _context.Teachers.Any(t => t.TeacherId == teacherId);
        }

        public bool UpdateTeacher(Teacher teacher)
        {
            _context.Update(teacher);

            return Save();
        }
    }
}
