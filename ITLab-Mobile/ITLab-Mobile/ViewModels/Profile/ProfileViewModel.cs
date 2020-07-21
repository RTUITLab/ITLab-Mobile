using IdentityModel.OidcClient;
using ITLab_Mobile.Api;
using ITLab_Mobile.Api.Models.User;
using ITLab_Mobile.Services;
using ITLab_Mobile.Views;
using Refit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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

        public Command RefreshCommand { get; set; }
        public Command LogoutCommand { get; set; }

        public INavigation Navigation { get; set; }

        private UserView user;
        public UserView User
        {
            get => user;
            set { SetProperty(ref user, value); }
        }

        private readonly HttpClient httpClient;
        public ProfileViewModel()
        {
            Title = "Profile";

            httpClient = App.ServiceProvider.GetService<IHttpClientFactory>().CreateClient(Settings.HttpClientName);

            RefreshCommand = new Command(async () => await GetProfileInfoAsync());
            LogoutCommand = new Command(async () => await LogoutAsync());
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
