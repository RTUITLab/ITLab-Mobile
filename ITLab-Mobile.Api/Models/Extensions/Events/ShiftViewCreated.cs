using System;
using System.Collections.Generic;

namespace ITLab_Mobile.Api.Models.Extensions.Events
{
    public class ShiftViewCreated
    {
        public int ClientId { get; set; }
        public Guid Id { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public List<PlaceViewCreated> Places { get; set; }
    }
}
