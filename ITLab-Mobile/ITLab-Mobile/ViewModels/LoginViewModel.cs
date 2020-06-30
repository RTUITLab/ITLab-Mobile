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
        public INavigation Navigation { get; set; }

        public LoginViewModel()
        {
            Title = "Login";

            LoginCommand = new Command(async () => await Login());
        }

        OidcClient OidcClient;

        async Task Login()
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

                if (request.IsError)
                {
                    Debug.WriteLine("Error while logging");
                    return;
                }

                await Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            IsBusy = false;
        }
    }
}
