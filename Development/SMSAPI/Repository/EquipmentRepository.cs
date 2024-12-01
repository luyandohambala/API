using SMSAPI.Data;
using SMSAPI.Interfaces;
using SMSAPI.Models;

namespace SMSAPI.Repository
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly DataContext _context;

        public EquipmentRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateEquipment(Equipment equipment)
        {
            _context.Add(equipment);

            return Save();
        }

        public bool DeleteEquipment(Equipment equipment)
        {
            _context.Remove(equipment);

            return Save();
        }

        public bool EquipmentExists(Guid equipmentId)
        {
            return _context.Equipment.Any(e => e.EquipmentId == equipmentId);
        }

        public Guid GetDepartmentIdByEquipmentId(Guid equipmentId)
        {
            return _context.Equipment.Where(e => e.EquipmentId == equipmentId).Select(e => e.EquipmentDepartmentId).FirstOrDefault();
        }

        public ICollection<Equipment> GetEquipment()
        {
            return [.. _context.Equipment.OrderBy(e => e.EquipmentId)];
        }

        public Equipment GetEquipmentById(Guid equipmentId)
        {
            return _context.Equipment.Where(e => e.EquipmentId == equipmentId).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool UpdateEquipment(Equipment equipment)
        {
            _context.Update(equipment);

            return Save();
        }
    }
}
