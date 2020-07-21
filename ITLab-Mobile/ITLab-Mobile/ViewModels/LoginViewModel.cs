using System;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using ITLab_Mobile.Services;
using ITLab_Mobile.Views;

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

        async Task Login()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var request = await Settings.OidcClient.LoginAsync();

                if (request.IsError)
                {
                    Debug.WriteLine("Error while logging in");
                    return;
                }

                Settings.AccessToken = request.AccessToken;

                var userInfo = await Settings.OidcClient.GetUserInfoAsync(Settings.AccessToken);

                if (userInfo.IsError)
                {
                    Debug.WriteLine("Error while getting user info");
                    return;
                }

                if (Guid.TryParse(userInfo.Claims.FirstOrDefault(cl => cl.Type == "sub").Value, out Guid userId))
                {
                    Settings.CurrentUserId = userId;

                    Settings.RefreshToken = request.RefreshToken;
                    Settings.IdentityToken = request.IdentityToken;

                    Application.Current.MainPage = new NavigationPage(new MainPage());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            IsBusy = false;
        }
    }
}
