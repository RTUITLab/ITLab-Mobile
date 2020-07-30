using ITLab_Mobile.Api.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ITLab_Mobile.Api.Models.Extensions.User
{
    public class UserViewExtended : UserView
    {
        public bool IsMiddleNameSet
            => !string.IsNullOrEmpty(MiddleName);

        public ICommand AddToContacts { get; set; }
        public ICommand Call { get; set; }
        public ICommand SendMessage { get; set; }
        public ICommand SendEmail { get; set; }
    }
}
