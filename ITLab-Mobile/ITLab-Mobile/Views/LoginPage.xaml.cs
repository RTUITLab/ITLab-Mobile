using ITLab_Mobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ITLab_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel loginViewModel;

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = loginViewModel = new LoginViewModel(Navigation);
        }
    }
}