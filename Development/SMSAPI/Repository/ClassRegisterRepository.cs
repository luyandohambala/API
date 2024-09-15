using SMSAPI.Data;
using SMSAPI.Interfaces;
using SMSAPI.Models;

namespace SMSAPI.Repository
{
    public class ClassRegisterRepository : IClassRegisterRepository
    {
        private readonly DataContext _context;

        public ClassRegisterRepository(DataContext context)
        {
            _context = context;
        }

        public bool ClassRoomRegisterExists(Guid classRoomRegisterId)
        {
            return _context.ClassRegisters.Any(crg => crg.ClassRegisterId == classRoomRegisterId);
        }

        public bool CreateClassRegister(ClassRegister classRoomRegister)
        {
            _context.Add(classRoomRegister);

            return Save();
        }

        public bool DeleteClassRegister(ClassRegister classRoomRegister)
        {
            _context.Remove(classRoomRegister);

            return Save();
        }

        public ClassRegister GetClassRegisterByClassId(Guid classRoomRegisterId)
        {
            return _context.ClassRegisters.Where(crg => crg.ClassRegisterId == classRoomRegisterId).FirstOrDefault();
        }

        public ICollection<ClassRegister> GetClassRegisters()
        {
            return [.. _context.ClassRegisters.OrderBy(crg => crg.ClassRegisterId)];
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool UpdateClassRegister(ClassRegister classRoomRegister)
        {
            _context.Update(classRoomRegister);

            return Save();
        }
    }
}
