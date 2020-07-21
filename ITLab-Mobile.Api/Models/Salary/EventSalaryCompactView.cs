using System;

namespace ITLab_Mobile.Api.Models.Salary
{
    public class EventSalaryCompactView
    {
        public Guid EventId { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
    }
}
