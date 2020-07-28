using ITLab_Mobile.ViewModels.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ITLab_Mobile.Views.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        ProfileViewModel profileViewModel;

        public ProfilePage()
        {
            InitializeComponent();

            BindingContext = profileViewModel = new ProfileViewModel
            {
                Navigation = this.Navigation
            };
        }
    }
}