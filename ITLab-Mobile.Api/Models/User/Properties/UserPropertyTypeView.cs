using System;

namespace ITLab_Mobile.Api.Models.User.Properties
{
    public class UserPropertyTypeView
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int InstancesCount { get; set; }
        public bool IsLocked { get; set; }
    }
}
