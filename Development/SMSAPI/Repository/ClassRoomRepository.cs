using Microsoft.EntityFrameworkCore;
using SMSAPI.Data;
using SMSAPI.Interfaces;
using SMSAPI.Models;

namespace SMSAPI.Repository
{
    public class ClassRoomRepository : IClassRoomRepository
    {
        private readonly DataContext _context;

        public ClassRoomRepository(DataContext context)
        {
            _context = context;
        }

        public bool ClassRoomExists(Guid classRoomId)
        {
            return _context.ClassRooms.Any(cr => cr.ClassId == classRoomId);
        }

        public bool CreateClassRoom(ClassRoom classRoom)
        {
            _context.Add(classRoom);

            return Save();
        }

        public bool DeleteClassRoom(ClassRoom classRoomId)
        {
            _context.Remove(classRoomId);

            return Save();
        }

        public bool DeleteClassRooms(List<ClassRoom> classRooms)
        {
            _context.RemoveRange(classRooms);

            return Save();
        }

        public ClassRoom GetClassRoom(Guid classRoomId)
        {
            return _context.ClassRooms.Where(cr => cr.ClassId == classRoomId).Include(s => s.Subjects).FirstOrDefault();
        }

        public ICollection<ClassRoom> GetClassRooms()
        {
            return [.. _context.ClassRooms.OrderBy(cr => cr.ClassId)];
        }

        public Guid GetGradeByClassRoomId(Guid classRoomId)
        {
            return _context.ClassRooms.Where(cr => cr.ClassId == classRoomId).Select(g => g.ClassGradeId).FirstOrDefault();
        }

        public ICollection<Pupil> GetPupilsByClassRoomId(Guid classRoomId)
        {
            return [.. _context.Pupils.Where(cr => cr.PupilClassId == classRoomId)];
        }

        public ICollection<Subject> GetSubjectsByClassRoomId(Guid classRoomId)
        {
            return [.. _context.ClassRoomSubjects.Where(cr => cr.ClassRoomId == classRoomId).Select(s => s.Subject)];
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool UpdateClassRoom(ClassRoom classRoom)
        {
            _context.Update(classRoom);

            return Save();
        }
    }
}
