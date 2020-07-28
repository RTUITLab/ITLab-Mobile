using ITLab_Mobile.Api;
using ITLab_Mobile.Api.Models.Event;
using ITLab_Mobile.Api.Models.Event.EventType;
using ITLab_Mobile.Api.Models.Salary;
using ITLab_Mobile.Services;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace ITLab_Mobile.ViewModels.Events
{
    public class CreateEventViewModel : BaseViewModel
    {
        public ICommand CreateCommand { get; set; }
        public ICommand AddShiftCommand { get; set; }

        private ObservableCollection<EventTypeView> eventTypes;
        public ObservableCollection<EventTypeView> EventTypes
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

        private EventSalaryCreateEdit eventSalaryCreateEdit;
        public EventSalaryCreateEdit Salary
        {
            get => eventSalaryCreateEdit;
            set { SetProperty(ref eventSalaryCreateEdit, value); }
        }

        private readonly HttpClient httpClient;
        public CreateEventViewModel()
        {
            Title = "Create event";

            httpClient = App.ServiceProvider.GetService<IHttpClientFactory>().CreateClient(Settings.HttpClientName);

            CreateCommand = new Command(async () => await CreateEvent());
            AddShiftCommand = new Command(AddShift);

            EventTypes = new ObservableCollection<EventTypeView>();
            Event = new EventCreateRequest();
            Shifts = new ObservableCollection<ShiftCreateRequestObservable>();
            Salary = new EventSalaryCreateEdit
            {
                Count = 0
            };
        }

        async Task CreateEvent()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                if (EventTypes.Count != 1)
                {
                    EventTypes = new ObservableCollection<EventTypeView>();
                    EventTypes.Add(new EventTypeView
                    {
                        Title = "ВЫБЕРИТЕ ТИП СОБЫТИЯ"
                    });
                    IsBusy = false;
                    return;
                }

                Event.EventTypeId = EventTypes[0].Id;

                Event.Shifts = new List<ShiftCreateRequest>();
                Shifts.ForEach(sh =>
                {
                    var shift = new ShiftCreateRequest
                    {
                        ClientId = sh.ClientId,
                        BeginTime = sh.BeginDate + sh.BeginTime,
                        EndTime = sh.EndDate + sh.EndTime,
                        Description = sh.Description,
                        Places = new List<PlaceCreateRequest>()
                    };
                    sh.Places.ForEach(pl =>
                    {
                        shift.Places.Add(new PlaceCreateRequest
                        {
                            ClientId = pl.ClientId,
                            Description = pl.Description,
                            TargetParticipantsCount = pl.TargetParticipantsCount
                        });
                    });
                    Event.Shifts.Add(shift);
                });

                var eventApi = RestService.For<IEventApi>(httpClient);
                var createdEvent = await eventApi.CreateEvent(Event);

                Salary.ShiftSalaries = new List<ShiftSalaryEdit>();
                Salary.PlaceSalaries = new List<PlaceSalaryEdit>();
                createdEvent.Shifts.ForEach(sh =>
                {
                    var shift = Shifts.FirstOrDefault(s => s.ClientId == sh.ClientId);
                    for (int i = 0; i < shift.Places.Count; i++)
                    {
                        Salary.PlaceSalaries.Add(new PlaceSalaryEdit
                        {
                            Count = shift.Places[i].SalaryCount,
                            Description = shift.Places[i].SalaryDescription,
                            PlaceId = sh.Places[i].Id
                        });
                    }
                    Salary.ShiftSalaries.Add(new ShiftSalaryEdit
                    {
                        Count = shift.SalaryCount,
                        Description = shift.SalaryDescription,
                        ShiftId = sh.Id
                    });
                });

                var salaryApi = RestService.For<ISalaryApi>(httpClient);
                var createdSalary = await salaryApi.CreateEditSalary(createdEvent.Id, Salary);
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
                var types = await eventApi.GetEventTypes(EventTypeName);
                EventTypes = new ObservableCollection<EventTypeView>(types);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private int clientId = 0;
        void AddShift()
        {
            var newShift = new ShiftCreateRequestObservable
            {
                ClientId = clientId,
                BeginDate = DateTime.Today,
                BeginTime = DateTime.Now.TimeOfDay,
                EndDate = DateTime.Today,
                EndTime = DateTime.Now.TimeOfDay,
                Places = new ObservableCollection<PlaceCreateRequestObservable>(),
            };
            newShift.DeleteShift = new Command(() => Shifts.Remove(newShift));
            Shifts.Add(newShift);
            clientId++;
        }
    }
}
