using ITLab_Mobile.Api;
using ITLab_Mobile.Api.Models.Extensions.Events;
using ITLab_Mobile.Services;
using Models.PublicAPI.Responses.Event;
using Refit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ITLab_Mobile.ViewModels.Events
{
    public class OneEventViewModel : BaseViewModel
    {
        private EventViewExtended oneEvent;
        public EventViewExtended Event 
        {
            get => oneEvent;
            set { SetProperty(ref oneEvent, value); }
        }

        public INavigation Navigation { get; set; }
        public Guid EventId { get; set; }
        private readonly HttpClient httpClient;

        public OneEventViewModel(Guid eventId)
        {
            Title = "Event";

            EventId = eventId;
            httpClient = App.ServiceProvider.GetService<IHttpClientFactory>().CreateClient(Settings.HttpClientName);
            GetEventAsync();
        }

        public async Task<List<EventRoleView>> GetEventRoles()
        {
            try
            {
                var eventApi = RestService.For<IEventApi>(httpClient);
                return await eventApi.GetEventRoles();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return new List<EventRoleView>
            {
                new EventRoleView
                {
                    Title = "Не удалось загрузить данные"
                }
            };
        }

        public async Task<string> SendWishAsync(Guid placeId, Guid roleId)
        {
            try
            {
                var eventApi = RestService.For<IEventApi>(httpClient);
                var result = await eventApi.SendWishAsync(placeId, roleId);
                if (result.IsSuccessStatusCode)
                    return "Заявка отправлена";

                return $"{result.StatusCode} -- {result.Error.Content}";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return "Что-то пошло не так";
        }

        async Task GetEventAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var eventApi = RestService.For<IEventApi>(httpClient);
                Event = await eventApi.GetOneEvent(EventId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            IsBusy = false;

            await GetSalaryAsync();
        }

        async Task GetSalaryAsync()
        {
            try
            {
                var salaryApi = RestService.For<ISalaryApi>(httpClient);
                var salaryRaw = await salaryApi.GetOneSalary(EventId);

                if (salaryRaw.IsSuccessStatusCode)
                {
                    var salary = salaryRaw.Content;
                    Event.Salary = salary.Count.ToString() + " ₽";

                    Event.ShiftsGrouped.ForEach(shift =>
                    {
                        foreach (var place in shift)
                        {
                            var salaryPlace = salary.PlaceSalaries.FirstOrDefault(pl => pl.PlaceId == place.Id);
                            if (salaryPlace == null)
                            {
                                place.Salary = "Оплата не указана";
                            }
                            else
                            {
                                place.Salary = salaryPlace.Count.ToString() + " ₽";
                            }
                        }

                        var salaryShift = salary.ShiftSalaries.FirstOrDefault(sh => sh.ShiftId == shift.Id);
                        if (salaryShift == null)
                        {
                            shift.Salary = "Оплата не указана";
                        }
                        else
                        {
                            shift.Salary = salaryShift.Count.ToString() + " ₽";
                        }
                    });
                }
                else
                {
                    Event.Salary = "Оплата не указана";

                    Event.ShiftsGrouped.ForEach(shift =>
                    {
                        foreach (var place in shift)
                        {
                            place.Salary = "Оплата не указана";
                        }
                        shift.Salary = "Оплата не указана";
                    });
                }
                OnPropertyChanged(nameof(Event));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
