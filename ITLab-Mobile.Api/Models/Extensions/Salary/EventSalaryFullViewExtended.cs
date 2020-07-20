using ITLab_Mobile.Api.Models.Salary;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITLab_Mobile.Api.Models.Extensions.Salary
{
    public class EventSalaryFullViewExtended : EventSalaryFullView
    {
        public bool IsDescription
            => !string.IsNullOrEmpty(Description);
    }
}
