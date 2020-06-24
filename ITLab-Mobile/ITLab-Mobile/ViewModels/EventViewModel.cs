using ITLab_Mobile.Api;
using ITLab_Mobile.Api.Models.Extensions;
using ITLab_Mobile.Services;
using Models.PublicAPI.Responses.Event;
using Refit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ITLab_Mobile.ViewModels
{
    public class EventViewModel : BaseViewModel
    {
        public Command EventCommand { get; set; }
        public List<CompactEventViewExtended> Events { get; set; }
        public bool IsRefreshing { get; set; }

        public EventViewModel()
        {
            Title = "Events";

            EventCommand = new Command(async () => await GetEventsAsync());
            IsRefreshing = false;
            GetEventsAsync();
        }

        async Task GetEventsAsync()
        {
            if (IsBusy)
                return;

            IsBusy = IsRefreshing = true;

            try
            {
                var eventApi = RestService.For<IEventApi>(HttpClientFactory.HttpClient);
                Events = await eventApi.GetEvents();
                OnPropertyChanged(nameof(Events));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            IsBusy = IsRefreshing = false;
            OnPropertyChanged(nameof(IsRefreshing));
        }
    }
}