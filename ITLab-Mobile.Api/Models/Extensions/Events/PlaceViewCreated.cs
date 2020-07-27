using ITLab_Mobile.Api.Models.Equipment;
using Models.PublicAPI.Responses.Event;
using System;
using System.Collections.Generic;

namespace ITLab_Mobile.Api.Models.Extensions.Events
{
    public class PlaceViewCreated
    {
        public int ClientId { get; set; }
        public Guid Id { get; set; }
        public int TargetParticipantsCount { get; set; }
        public string Description { get; set; }
        public List<EquipmentView> Equipment { get; set; }
        public List<UserAndEventRole> Participants { get; set; }
        public List<UserAndEventRole> Invited { get; set; }
        public List<UserAndEventRole> Wishers { get; set; }
        public List<UserAndEventRole> Unknowns { get; set; }
    }
}
