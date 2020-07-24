using System;

namespace ITLab_Mobile.Api.Models.Salary
{
    public class PlaceSalaryEdit
    {
        public Guid PlaceId { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
    }
}
