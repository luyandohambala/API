using SMSAPI.Data;
using SMSAPI.Interfaces;
using SMSAPI.Models;

namespace SMSAPI.Repository
{
    public class GradeRepository : IGradeRepository
    {
        private readonly DataContext _context;

        public GradeRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateGrade(Grade grade)
        {
            _context.Add(grade);

            return Save();
        }

        public bool DeleteGrade(Grade grade)
        {
            _context.Remove(grade);

            return Save();
        }

        public ICollection<ClassRoom> GetClassRoomsByGradeId(Guid gradeId)
        {
            return [.. _context.ClassRooms.Where(gr => gr.ClassGradeId == gradeId)];
        }

        public Grade GetGrade(Guid gradeId)
        {
            return _context.Grades.Where(gr => gr.GradeId == gradeId).FirstOrDefault();
        }

        public ICollection<Grade> GetGrades()
        {
            return [.. _context.Grades.OrderBy(gr => gr.GradeId)];
        }

        public bool GradeExists(Guid gradeId)
        {
            return _context.Grades.Any(gr => gr.GradeId == gradeId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool UpdateGrade(Grade grade)
        {
            _context.Update(grade);

            return Save();
        }
    }
}
