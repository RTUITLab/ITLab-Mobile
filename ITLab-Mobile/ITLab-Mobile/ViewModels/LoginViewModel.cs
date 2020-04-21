using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using IdentityModel.OidcClient;
using ITLab_Mobile.Services;

namespace ITLab_Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public LoginViewModel(INavigation navigation)
        {
            Title = "Login";

            LoginCommand = new Command(async () => await MakeLogin());
            this.navigation = navigation;
        }

        OidcClient OidcClient;
        private readonly INavigation navigation;

        async Task MakeLogin()
        {   
            if (IsBusy)
                return;

            IsBusy = true;
            
            try
            {
                OidcClient = Settings.OidcClient;

                var request = await OidcClient.LoginAsync(new LoginRequest());
                Settings.AccessToken = request.AccessToken;
                Settings.RefreshToken = request.RefreshToken;

                // var a = request.RefreshTokenHandler;
                // a.InnerHandler = new HttpClientHandler();
                // var client = new HttpClient(a);
                // var respones = await client.GetStringAsync("https://dev.rtuitlab.ru/api/event/?begin=2020-04-02T14:16:19");

                if (request.IsError)
                {
                    Debug.WriteLine("Error while logging");
                    return;
                }

                await navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
