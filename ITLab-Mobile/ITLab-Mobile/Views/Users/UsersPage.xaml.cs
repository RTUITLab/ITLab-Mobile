using ITLab_Mobile.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ITLab_Mobile.Views.Users
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersPage : ContentPage
    {
        UsersViewModel usersViewModel;
        public UsersPage()
        {
            InitializeComponent();

            BindingContext = usersViewModel = new UsersViewModel();

        }

        async void Search_Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            await usersViewModel.GetUsersAsync();
        }
    }
}