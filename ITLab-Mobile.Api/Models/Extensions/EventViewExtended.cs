using Models.PublicAPI.Responses.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITLab_Mobile.Api.Models.Extensions
{
    public class EventViewExtended : EventView
    {
        public DateTime BeginTime
            => Shifts.FirstOrDefault().BeginTime;

        public DateTime EndTime
            => Shifts.LastOrDefault().EndTime;
    }
}
