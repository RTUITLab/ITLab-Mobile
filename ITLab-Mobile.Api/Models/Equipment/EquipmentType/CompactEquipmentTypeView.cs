using System;

namespace ITLab_Mobile.Api.Models.Equipment.EquipmentType
{
    public class CompactEquipmentTypeView
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ShortTitle { get; set; }
        public string Description { get; set; }
        public Guid RootId { get; set; }
        public Guid ParentId { get; set; }
    }
}
