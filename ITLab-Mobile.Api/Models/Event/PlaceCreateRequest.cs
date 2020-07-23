using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ITLab_Mobile.Api.Models.Event
{
    public class PlaceCreateRequest
    {
        public int ClientId { get; set; }
        public int TargetParticipantsCount { get; set; }
        public string Description { get; set; }

        public List<Guid> Equipment { get; set; }
        public List<PersonWorkRequest> Invited { get; set; }
    }

    public class PlaceCreateRequestObservable
    {
        public int ClientId { get; set; }
        public int TargetParticipantsCount { get; set; }
        public string Description { get; set; }

        public ObservableCollection<Guid> Equipment { get; set; }
        public ObservableCollection<PersonWorkRequest> Invited { get; set; }
    }
}
