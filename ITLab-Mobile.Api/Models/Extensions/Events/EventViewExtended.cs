using Models.PublicAPI.Responses.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITLab_Mobile.Api.Models.Extensions.Events
{
    public class EventViewExtended : EventView
    {
        public List<ShiftViewExtended> ShiftsEx
            => Shifts.OrderBy(key => key.BeginTime)
                    .Select(s => new ShiftViewExtended
                    {
                        Id = s.Id,
                        BeginTime = s.BeginTime,
                        EndTime = s.EndTime,
                        Description = s.Description,
                        Places = s.Places
                    })
                    .ToList();

        public DateTime BeginTime
            => ShiftsEx.FirstOrDefault().BeginTime;

        public DateTime EndTime
            => ShiftsEx.LastOrDefault().EndTime;

        public bool IsDescription
            => !string.IsNullOrEmpty(Description);
    }
}
