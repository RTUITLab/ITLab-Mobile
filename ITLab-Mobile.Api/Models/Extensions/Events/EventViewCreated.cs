using ITLab_Mobile.Api.Models.Event.EventType;
using System;
using System.Collections.Generic;

namespace ITLab_Mobile.Api.Models.Extensions.Events
{
    public class EventViewCreated
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public EventTypeView EventType { get; set; }
        public List<ShiftViewCreated> Shifts { get; set; }
    }
}
