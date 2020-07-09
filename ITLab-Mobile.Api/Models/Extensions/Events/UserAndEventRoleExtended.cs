using Models.PublicAPI.Responses.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITLab_Mobile.Api.Models.Extensions.Events
{
    public class UserAndEventRoleExtended : UserAndEventRole
    {
        public string PlaceRole { get; set; }
    }
}
