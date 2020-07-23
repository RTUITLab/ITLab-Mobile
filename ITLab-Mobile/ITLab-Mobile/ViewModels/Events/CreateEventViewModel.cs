using ITLab_Mobile.Api;
using ITLab_Mobile.Api.Models.Event;
using ITLab_Mobile.Api.Models.Event.EventType;
using ITLab_Mobile.Services;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ITLab_Mobile.ViewModels.Events
{
    public class CreateEventViewModel : BaseViewModel
    {
        public ICommand CreateCommand { get; set; }
        public ICommand AddShiftCommand { get; set; }

        private List<EventTypeView> eventTypes;
        public List<EventTypeView> EventTypes
        {
            get => eventTypes;
            set { SetProperty(ref eventTypes, value); }
        }

        private string eventTypeName;
        public string EventTypeName 
        { 
            get => eventTypeName;
            set { SetProperty(ref eventTypeName, value); } 
        }

        private EventCreateRequest eventCreateRequest;
        public EventCreateRequest Event
        {
            get => eventCreateRequest;
            set { SetProperty(ref eventCreateRequest, value); }
        }

        private ObservableCollection<ShiftCreateRequestObservable> shiftCreateRequests;
        public ObservableCollection<ShiftCreateRequestObservable> Shifts
        {
            get => shiftCreateRequests;
            set { SetProperty(ref shiftCreateRequests, value); }
        }

        private readonly HttpClient httpClient;
        public CreateEventViewModel()
        {
            Title = "Create event";

            httpClient = App.ServiceProvider.GetService<IHttpClientFactory>().CreateClient(Settings.HttpClientName);

            CreateCommand = new Command(async () => await CreateEvent());
            Shifts = new ObservableCollection<ShiftCreateRequestObservable>();
            AddShiftCommand = new Command(AddShift);
        }

        async Task CreateEvent()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            IsBusy = false;
        }

        public async Task UpdateEventTypes()
        {
            try
            {
                var eventApi = RestService.For<IEventApi>(httpClient);
                EventTypes = await eventApi.GetEventTypes(EventTypeName);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        void AddShift()
        {
            var clientId = Shifts.Count;
            Shifts.Add(new ShiftCreateRequestObservable
            {
                ClientId = clientId,
                BeginDate = DateTime.Today,
                BeginTime = DateTime.Now.TimeOfDay,
                EndDate = DateTime.Today,
                EndTime = DateTime.Now.TimeOfDay,
                Places = new ObservableCollection<PlaceCreateRequestObservable>()
            });
        }
    }
}
