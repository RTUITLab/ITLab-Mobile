using System.Collections.Generic;

namespace ITLab_Mobile.Api.Models.Salary
{
    public class EventSalaryCreateEdit
    {
        public int Count { get; set; }
        public string Description { get; set; }

        public List<ShiftSalaryEdit> ShiftSalaries { get; set; }
        public List<PlaceSalaryEdit> PlaceSalaries { get; set; }
    }
}
