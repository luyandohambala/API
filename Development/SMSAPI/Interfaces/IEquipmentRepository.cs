using SMSAPI.Models;

namespace SMSAPI.Interfaces
{
    public interface IEquipmentRepository
    {
        //read methods
        ICollection<Equipment> GetEquipment();

        Equipment GetEquipmentById(Guid equipmentId);

        Guid GetDepartmentIdByEquipmentId(Guid equipmentId);


        //write methods
        bool CreateEquipment(Equipment equipment);

        bool UpdateEquipment(Equipment equipment);

        bool Save();


        //delete methods
        bool DeleteEquipment(Equipment equipment);


        //validation methods
        bool EquipmentExists(Guid equipmentId);
    }
}
