using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ITLab_Mobile.Api.Models.Event
{
    public class ShiftCreateRequest
    {
        public int ClientId { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }

        public List<PlaceCreateRequest> Places { get; set; }
    }

    public class ShiftCreateRequestObservable
    {
        public ShiftCreateRequestObservable()
        {
            AddPlace = new Command(() =>
            {
                var clientId = Places.Count;
                Places.Add(new PlaceCreateRequestObservable
                {
                    ClientId = clientId,
                    TargetParticipantsCount = 1
                });
            });
        }

        public int ClientId { get; set; }
        public DateTime BeginDate { get; set; }
        public TimeSpan BeginTime { get; set; }

        public DateTime EndDate { get; set; }
        public TimeSpan EndTime { get; set; }

        public string Description { get; set; }

        public ObservableCollection<PlaceCreateRequestObservable> Places { get; set; }

        public ICommand AddPlace { get; set; }
    }
}
