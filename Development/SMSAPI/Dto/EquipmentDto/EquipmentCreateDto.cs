namespace SMSAPI.Dto.EquipmentDto
{
    public class EquipmentCreateDto
    {
        public Guid EquipmentId { get; set; }

        public string EquipmentName { get; set; }

        public string EquipmentType { get; set; }

        public string EquipmentQty { get; set; }

        public Guid EquipmentDepartmentId { get; set; }

        public string EquipmentStatus { get; set; }

    }
}
