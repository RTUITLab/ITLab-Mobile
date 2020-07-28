using ITLab_Mobile.Api.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITLab_Mobile.Api.Models.Extensions.User
{
    public class UserViewExtended : UserView
    {
        public bool IsMiddleNameSet
            => !string.IsNullOrEmpty(MiddleName);
    }
}
