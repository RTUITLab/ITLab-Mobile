using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using ITLab_Mobile.Services;

namespace ITLab_Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public LoginViewModel()
        {
            Title = "Login";

            LoginCommand = new Command(async () => await MakeLogin());
        }

        OidcClient OidcClient;

        async Task MakeLogin()
        {   
            if (IsBusy)
                return;

            IsBusy = true;
            
            try
            {
                var browser = DependencyService.Get<IBrowser>();

                OidcClient = new OidcClient(new OidcClientOptions
                {
                    Authority = "https://dev.identity.rtuitlab.ru",
                    ClientId = "interactive.public",
                    Scope = "openid profile email api offline_access",
                    RedirectUri = "xamarinformsclients://callback",
                    Browser = browser,

                    ResponseMode = OidcClientOptions.AuthorizeResponseMode.Redirect
                });

                var request = await OidcClient.LoginAsync(new LoginRequest());

                if (request.IsError)
                {
                    Debug.WriteLine("Error while logging");
                    return;
                }

                var sb = new StringBuilder(128);
                foreach (var claim in request.User.Claims)
                {
                    sb.AppendFormat("{0}: {1}\n", claim.Type, claim.Value);
                }

                sb.AppendFormat("\n{0}: {1}\n", "refresh token", request?.RefreshToken ?? "none");
                sb.AppendFormat("\n{0}: {1}\n", "access token", request.AccessToken);

                Debug.WriteLine(sb.ToString());

                Settings.AccessToken = request.AccessToken;
                Settings.RefreshToken = request.RefreshToken;

                //Debug.WriteLine(Login);
                //Debug.WriteLine(Password);
                //Thread.Sleep(3000);
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
