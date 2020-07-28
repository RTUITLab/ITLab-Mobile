using ITLab_Mobile.Api;
using ITLab_Mobile.Api.Models.Extensions.Events;
using ITLab_Mobile.Services;
using ITLab_Mobile.Views.Events;
using Refit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ITLab_Mobile.ViewModels.Events
{
    public class EventViewModel : BaseViewModel
    {
        private bool isRefreshing = false;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set { SetProperty(ref isRefreshing, value); }
        }

        public ICommand EventCommand { get; set; }
        public ICommand CreateEventCommand { get; set; }

        private List<CompactEventViewExtended> events = new List<CompactEventViewExtended>();
        public List<CompactEventViewExtended> Events
        {
            get => events;
            set { SetProperty(ref events, value); }
        }

        public INavigation Navigation { get; set; }

        private readonly HttpClient httpClient;

        public EventViewModel()
        {
            Title = "Events";

            EventCommand = new Command(async () => await GetEventsAsync());
            CreateEventCommand = new Command(async () => await Navigation.PushAsync(new CreateEventPage()));

            httpClient = App.ServiceProvider.GetService<IHttpClientFactory>().CreateClient(Settings.HttpClientName);

            GetEventsAsync();
        }

        private CompactEventViewExtended selectedEvent;
        public CompactEventViewExtended SelectedEvent
        {
            get => selectedEvent;
            set
            {
                SetProperty(ref selectedEvent, value);
                if (value != null)
                {
                    NavigateToEvent();
                    SelectedEvent = null;
                }
            }
        }

        async Task GetEventsAsync()
        {
            if (IsBusy)
                return;

            IsBusy = IsRefreshing = true;

            try
            {
                var eventApi = RestService.For<IEventApi>(httpClient);
                Events = (await eventApi.GetEvents())
                    .OrderByDescending(key => key.BeginTime)
                    .ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            IsBusy = IsRefreshing = false;

            await GetSalariesAsync();
        }

        async Task GetSalariesAsync()
        {
            try
            {
                var salaryApi = RestService.For<ISalaryApi>(httpClient);
                var salaries = await salaryApi.GetSalaries();

                Events.ForEach(ev =>
                {
                    var salary = salaries.FirstOrDefault(s => s.EventId == ev.Id);
                    if (salary == null)
                    {
                        ev.Salary = "Оплата не указана";
                    }
                    else
                    {
                        ev.Salary = salary.Count.ToString() + " ₽";
                    }
                    OnPropertyChanged(nameof(Events));
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        async Task NavigateToEvent()
        {
            await Navigation.PushAsync(new OneEventPage(selectedEvent.Id));
        }
    }
}