using ITLab_Mobile.Api.Models.Salary;
using System.Drawing;

namespace ITLab_Mobile.Api.Models.Extensions.Salary
{
    public class EventSalaryCompactViewExtended : EventSalaryCompactView
    {
        public Color BorderColor
            => Color.FromArgb(0, 123, 255);

        public bool IsDescription
            => !string.IsNullOrEmpty(Description);
    }
}
