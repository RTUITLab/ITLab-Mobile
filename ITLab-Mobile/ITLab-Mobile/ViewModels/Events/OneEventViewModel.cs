using ITLab_Mobile.Api;
using ITLab_Mobile.Api.Models.Extensions;
using ITLab_Mobile.Services;
using Models.PublicAPI.Responses.Event;
using Refit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ITLab_Mobile.ViewModels.Events
{
    public class OneEventViewModel : BaseViewModel
    {
        public EventViewExtended Event { get; set; }
        public INavigation Navigation { get; set; }
        public Guid EventId { get; set; }

        public OneEventViewModel(Guid eventId)
        {
            Title = "Event";

            EventId = eventId;
            GetEventAsync();
        }

        async Task GetEventAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var eventApi = RestService.For<IEventApi>(HttpClientFactory.HttpClient);
                Event = await eventApi.GetOneEvent(EventId);
                OnPropertyChanged(nameof(Event));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            IsBusy = false;
        }
    }
}
