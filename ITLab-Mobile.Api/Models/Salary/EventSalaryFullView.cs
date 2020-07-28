using System;
using System.Collections.Generic;

namespace ITLab_Mobile.Api.Models.Salary
{
    public class EventSalaryFullView
    {
        public Guid EventId { get; set; }
        public int Count { get; set; }
        public DateTime Created { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime ModificationDate { get; set; }
        public string Description { get; set; }

        public List<ShiftSalaryView> ShiftSalaries { get; set; }
        public List<PlaceSalaryView> PlaceSalaries { get; set; }
    }
}
