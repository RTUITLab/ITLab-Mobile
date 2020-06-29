using Models.PublicAPI.Responses.Event;
using System.Drawing;

namespace ITLab_Mobile.Api.Models.Extensions
{
    public class CompactEventViewExtended : CompactEventView
    {
        public Color BorderColor
            => Participating ? Color.FromArgb(40, 167, 69) : Color.FromArgb(0, 123, 255);
    }
}
