using ITLab_Mobile.Api;
using ITLab_Mobile.Api.Models.Extensions.User;
using ITLab_Mobile.Services;
using Plugin.Messaging;
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
using Xamarin.Forms.Internals;
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

        private readonly IPhoneCallTask phoneDialer;
        private readonly ISmsTask smsMessanger;
        private readonly IEmailTask emailMessanger;

        private readonly HttpClient httpClient;
        public UsersViewModel()
        {
            Title = "Users";

            httpClient = App.ServiceProvider.GetService<IHttpClientFactory>().CreateClient(Settings.HttpClientName);

            RefreshCommand = new Command(async () => await GetUsersAsync());

            phoneDialer = CrossMessaging.Current.PhoneDialer;
            smsMessanger = CrossMessaging.Current.SmsMessenger;
            emailMessanger = CrossMessaging.Current.EmailMessenger;

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

                var users = await userApi.GetUsers(SearchMatch, 10);

                users.ForEach(user => user.AddToContacts = new Command(async (u) => await AddToContactsAsync(u as UserViewExtended)));
                users.ForEach(user => user.Call = new Command(async (u) => await CallAsync(u as UserViewExtended)));
                users.ForEach(user => user.SendMessage = new Command(async (u) => await SendMessageAsync(u as UserViewExtended)));
                users.ForEach(user => user.SendEmail = new Command(async (u) => await SendEmailAsync(u as UserViewExtended)));

                Users = new ObservableCollection<UserViewExtended>(users);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            IsBusy = IsRefreshing = false;
        }

        async Task AddToContactsAsync(UserViewExtended user)
        {
            DependencyService.Get<IContact>()?.SaveContacts(user.FirstName, user.MiddleName, user.LastName, user.Email, user.PhoneNumber);
        }

        async Task CallAsync(UserViewExtended user)
        {
            if (phoneDialer.CanMakePhoneCall)
            {
                phoneDialer.MakePhoneCall(user.PhoneNumber);
            }
        }

        async Task SendMessageAsync(UserViewExtended user)
        {
            if (smsMessanger.CanSendSms)
            {
                smsMessanger.SendSms(user.PhoneNumber);
            }
        }

        async Task SendEmailAsync(UserViewExtended user)
        {
            if (emailMessanger.CanSendEmail)
            {
                emailMessanger.SendEmail(to: user.Email);
            }
        }
    }
}
