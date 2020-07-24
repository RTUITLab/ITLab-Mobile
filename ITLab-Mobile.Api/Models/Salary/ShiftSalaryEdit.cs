using System;

namespace ITLab_Mobile.Api.Models.Salary
{
    public class ShiftSalaryEdit
    {
        public Guid ShiftId { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
    }
}
