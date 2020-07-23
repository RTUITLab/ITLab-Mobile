using System;
using System.Collections.Generic;

namespace ITLab_Mobile.Api.Models.Event
{
    public class EventCreateRequest
    {
        public string Title { get; set; }
        public Guid EventTypeId { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }

        public List<ShiftCreateRequest> Shifts { get; set; }
    }
}
