using SMSAPI.Data;
using SMSAPI.Interfaces;
using SMSAPI.Models;

namespace SMSAPI.Repository
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly DataContext _context;

        public RegisterRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateRegisterPupil(Register pupilRegister)
        {
            _context.Add(pupilRegister);

            return Save();
        }

        public bool DeleteRegisterPupil(Register pupilRegister)
        {
            _context.Remove(pupilRegister);

            return Save();
        }

        public Register GetPupilFromRegister(Guid pupilRegisterId)
        {
            return _context.Registers.Where(r => r.RegisterPupilId == pupilRegisterId).FirstOrDefault();
        }

        public ICollection<Register> GetPupilsFromRegister()
        {
            return [.. _context.Registers.OrderBy(r => r.RegisterPupilId)];
        }

        public ICollection<Register> GetRegisterByClassId(Guid classRoomRegisterId)
        {
            return [.. _context.ClassRoomRegisters.Where(crr => crr.ClassRoomId == classRoomRegisterId).Select(r => r.Register)];
        }

        public bool RegisterPupilExists(Guid pupilRegisterId)
        {
            return _context.Registers.Any(r => r.RegisterPupilId == pupilRegisterId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool UpdateRegisterPupil(Register pupilRegister)
        {
            _context.Update(pupilRegister);

            return Save();
        }
    }
}
