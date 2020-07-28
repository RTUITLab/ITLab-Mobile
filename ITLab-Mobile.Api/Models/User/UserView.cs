using ITLab_Mobile.Api.Models.User.Properties;
using System;
using System.Collections.Generic;

namespace ITLab_Mobile.Api.Models.User
{
    public class UserView
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public List<UserPropertyView> Properties { get; set; }
    }
}
