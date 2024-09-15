using SMSAPI.Data;
using SMSAPI.Interfaces;
using SMSAPI.Models;

namespace SMSAPI.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly DataContext _context;

        public SubjectRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateSubject(Subject subject)
        {
            _context.Add(subject);

            return Save();
        }

        public bool DeleteSubject(Subject subjectId)
        {
            _context.Remove(subjectId);

            return Save();
        }

        public ICollection<ClassRoom> GetClassRoomsBySubjectId(Guid subjectId)
        {
            return [.. _context.ClassRoomSubjects.Where(s => s.SubjectId == subjectId).Select(cr => cr.ClassRoom)];
        }

        public Subject GetSubject(Guid subjectId)
        {
            return _context.Subjects.Where(s => s.SubjectId == subjectId).FirstOrDefault();
        }

        public ICollection<Subject> GetSubjects()
        {
            return [.. _context.Subjects.OrderBy(s => s.SubjectId)];    
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool SubjectExists(Guid subject)
        {
            return _context.Subjects.Any(s => s.SubjectId == subject);   
        }

        public bool UpdateSubject(Subject subject)
        {
            _context.Update(subject);

            return Save();
        }
    }
}
