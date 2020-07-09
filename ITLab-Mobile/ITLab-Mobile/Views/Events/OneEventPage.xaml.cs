using ITLab_Mobile.Api.Models.Extensions.Events;
using ITLab_Mobile.ViewModels.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ITLab_Mobile.Views.Events
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OneEventPage : ContentPage
    {
        public Guid EventId { get; set; }

        OneEventViewModel oneEventViewModel;

        public OneEventPage(Guid eventId)
        {
            InitializeComponent();

            EventId = eventId;
            BindingContext = oneEventViewModel = new OneEventViewModel(EventId)
            {
                Navigation = this.Navigation
            };

            MessagingCenter.Subscribe<PlaceViewExtended>(this, "wish", (place) => WishAsync(place));
        }

        private async Task WishAsync(PlaceViewExtended place)
        {
            if (oneEventViewModel.IsBusy)
                return;

            oneEventViewModel.IsBusy = true;

            string cancel = "Отмена";
            try
            {
                string title = "Выберите роль";
                var eventRoles = await oneEventViewModel.GetEventRoles();
                string role = await DisplayActionSheet(title, cancel, null, eventRoles.Select(er => er.Title).ToArray());

                if (string.IsNullOrEmpty(role) || role.Equals(cancel))
                {
                    await DisplayAlert("Ошибка", "Необходимо выбрать роль", cancel);
                }

                Guid eventRoleId = eventRoles
                                    .Where(er => er.Title == role)
                                    .SingleOrDefault()
                                    .Id;

                string answer = await oneEventViewModel.SendWishAsync(place.Id, eventRoleId);
                await DisplayAlert("", answer, cancel);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await DisplayAlert("Ошибка", "Что-то пошло не так", cancel);
            }

            oneEventViewModel.IsBusy = false;
        }
    }
}