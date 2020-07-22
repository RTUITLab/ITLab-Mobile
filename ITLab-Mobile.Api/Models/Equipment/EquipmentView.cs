using ITLab_Mobile.Api.Models.Equipment.EquipmentType;
using System;

namespace ITLab_Mobile.Api.Models.Equipment
{
    public class EquipmentView
    {
        public Guid Id { get; set; }
        public string SerialNumber { get; set; }
        public string Description { get; set; }
        public int Number { get; set; }

        public CompactEquipmentTypeView EquipmentType { get; set; }

        public Guid EquipmentTypeId { get; set; }
        public Guid OwnerId { get; set; }
        public Guid ParentId { get; set; }
    }
}
