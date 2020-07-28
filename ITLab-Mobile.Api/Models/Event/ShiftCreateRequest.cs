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
        public ICommand AddPlace { get; set; }
        public ICommand DeleteShift { get; set; }
        private int clientId = 0;
        public ShiftCreateRequestObservable()
        {
            AddPlace = new Command(() =>
            {
                var newPlace = new PlaceCreateRequestObservable
                {
                    Title = $"Место #{Places.Count + 1}",
                    ClientId = clientId,
                    TargetParticipantsCount = 0
                };
                newPlace.DeletePlace = new Command(() => Places.Remove(newPlace));
                Places.Add(newPlace);
                clientId++;
            });
        }

        public int ClientId { get; set; }
        public DateTime BeginDate { get; set; }
        public TimeSpan BeginTime { get; set; }

        public DateTime EndDate { get; set; }
        public TimeSpan EndTime { get; set; }

        public string Description { get; set; }

        public ObservableCollection<PlaceCreateRequestObservable> Places { get; set; }

        // Salary
        public int SalaryCount { get; set; }
        public string SalaryDescription { get; set; }
    }
}
