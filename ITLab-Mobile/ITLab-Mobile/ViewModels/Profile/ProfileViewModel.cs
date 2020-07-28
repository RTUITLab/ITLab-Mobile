using IdentityModel.OidcClient;
using ITLab_Mobile.Api;
using ITLab_Mobile.Api.Models.Equipment;
using ITLab_Mobile.Api.Models.User;
using ITLab_Mobile.Services;
using ITLab_Mobile.Views;
using Refit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ITLab_Mobile.ViewModels.Profile
{
    public class ProfileViewModel : BaseViewModel
    {
        private bool isRefreshing = false;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set { SetProperty(ref isRefreshing, value); }
        }

        public ICommand RefreshCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        public INavigation Navigation { get; set; }

        private UserView user;
        public UserView User
        {
            get => user;
            set { SetProperty(ref user, value); }
        }

        private List<EquipmentView> equipments;
        public List<EquipmentView> Equipments
        {
            get => equipments;
            set { SetProperty(ref equipments, value); }
        }

        private readonly HttpClient httpClient;
        public ProfileViewModel()
        {
            Title = "Profile";

            httpClient = App.ServiceProvider.GetService<IHttpClientFactory>().CreateClient(Settings.HttpClientName);

            RefreshCommand = new Command(async () => await GetProfileInfoAsync());
            LogoutCommand = new Command(async () => await LogoutAsync());
            SaveCommand = new Command(async () => await SaveInfoAsync());
            GetProfileInfoAsync();
        }

        async Task GetProfileInfoAsync()
        {
            if (IsBusy)
                return;

            IsBusy = IsRefreshing = true;

            try
            {
                var userApi = RestService.For<IUserApi>(httpClient);
                User = await userApi.GetUser(Settings.CurrentUserId);
                await GetUserEquipmentAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            IsBusy = IsRefreshing = false;

        }

        async Task GetUserEquipmentAsync()
        {
            try
            {
                var equipmentApi = RestService.For<IEquipmentApi>(httpClient);
                Equipments = await equipmentApi.GetUserEquipment(User.Id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        async Task SaveInfoAsync()
        {
            if (IsBusy)
                return;

            IsBusy = IsRefreshing = true;

            try
            {
                var userApi = RestService.For<IUserApi>(httpClient);

                User = await userApi.EditUserInfo(new UserInfoEditRequest
                {
                    FirstName = User.FirstName,
                    LastName = User.LastName,
                    MiddleName = User.MiddleName,
                    PhoneNumber = User.PhoneNumber
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            IsBusy = IsRefreshing = false;
        }

        async Task LogoutAsync()
        {
            try
            {
                var request = await Settings.OidcClient.LogoutAsync(new LogoutRequest
                {
                    IdTokenHint = Settings.IdentityToken
                });

                if (request.IsError)
                {
                    Debug.WriteLine("Error while logging out");
                    return;
                }

                Settings.AccessToken = "";
                Settings.RefreshToken = "";
                Settings.IdentityToken = "";

                Application.Current.MainPage = new LoginPage();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
