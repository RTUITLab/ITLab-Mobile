using Models.PublicAPI.Responses.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITLab_Mobile.Api.Models.Extensions.Events
{
    public class EventViewExtended : EventView
    {
        public List<ShiftViewGrouped> ShiftsGrouped
            => Shifts.OrderBy(key => key.BeginTime)
                    .Select(s => new ShiftViewGrouped(s.Places)
                    {
                        Id = s.Id,
                        BeginTime = s.BeginTime,
                        EndTime = s.EndTime,
                        Description = s.Description,
                    })
                    .ToList();

        public DateTime BeginTime
            => ShiftsGrouped.FirstOrDefault().BeginTime;

        public DateTime EndTime
            => ShiftsGrouped.LastOrDefault().EndTime;

        public bool IsDescription
            => !string.IsNullOrEmpty(Description);
    }
}
