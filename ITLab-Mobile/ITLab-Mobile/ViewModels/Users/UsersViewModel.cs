using ITLab_Mobile.Api;
using ITLab_Mobile.Api.Models.Extensions.User;
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

namespace ITLab_Mobile.ViewModels.Users
{
    public class UsersViewModel : BaseViewModel
    {
        private bool isRefreshing = false;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set { SetProperty(ref isRefreshing, value); }
        }

        public ICommand RefreshCommand { get; set; }

        private ObservableCollection<UserViewExtended> users = null;
        public ObservableCollection<UserViewExtended> Users
        {
            get => users;
            set { SetProperty(ref users, value); }
        }

        private string searchMatch = "";
        public string SearchMatch
        {
            get => searchMatch;
            set { SetProperty(ref searchMatch, value); }
        }

        private readonly HttpClient httpClient;
        public UsersViewModel()
        {
            Title = "Users";

            httpClient = App.ServiceProvider.GetService<IHttpClientFactory>().CreateClient(Settings.HttpClientName);

            RefreshCommand = new Command(async () => await GetUsersAsync());

            GetUsersAsync();
        }

        public async Task GetUsersAsync()
        {
            if (IsBusy)
                return;

            IsBusy = IsRefreshing = true;

            try
            {
                var userApi = RestService.For<IUserApi>(httpClient);
                //Users.Clear();
                //foreach (var user in await userApi.GetUsers(SearchMatch, 10))
                //{
                //    Users.Add(user);
                //}
                var users = await userApi.GetUsers(SearchMatch, 10);
                Users = new ObservableCollection<UserViewExtended>(users);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            IsBusy = IsRefreshing = false;
        }
    }
}
