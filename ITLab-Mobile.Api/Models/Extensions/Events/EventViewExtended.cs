using Models.PublicAPI.Responses.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITLab_Mobile.Api.Models.Extensions.Events
{
    public class EventViewExtended : EventView
    {
        private Lazy<List<ShiftViewGrouped>> shiftsGrouped;

        public List<ShiftViewGrouped> ShiftsGrouped
            => shiftsGrouped.Value;

        public DateTime BeginTime
            => ShiftsGrouped.FirstOrDefault().BeginTime;

        public DateTime EndTime
            => ShiftsGrouped.LastOrDefault().EndTime;

        public bool IsDescription
            => !string.IsNullOrEmpty(Description);

        public string Salary { get; set; } = "No data";
        public bool IsSalaryLoaded
        {
            get
            {
                if (Salary.Equals("No data"))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public EventViewExtended()
        {
            shiftsGrouped = new Lazy<List<ShiftViewGrouped>>(() =>
            {
                return Shifts.OrderBy(key => key.BeginTime)
                        .Select(s => new ShiftViewGrouped(s.Places)
                        {
                            Id = s.Id,
                            BeginTime = s.BeginTime,
                            EndTime = s.EndTime,
                            Description = s.Description,
                        })
                        .ToList();
            });
        }
    }
}
