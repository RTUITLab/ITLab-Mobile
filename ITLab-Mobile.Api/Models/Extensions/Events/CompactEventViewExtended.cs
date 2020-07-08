using ITLab_Mobile.Api.Models.Helpers;
using Models.PublicAPI.Responses.Event;
using System;
using System.Drawing;

namespace ITLab_Mobile.Api.Models.Extensions.Events
{
    public class CompactEventViewExtended : CompactEventView
    {
        public Color BorderColor
            => Participating ? Color.FromArgb(40, 167, 69) : Color.FromArgb(0, 123, 255);

        public double ProgressToBar
            => TargetParticipantsCount <= 0 ? 1 : Convert.ToDouble(CurrentParticipantsCount) / TargetParticipantsCount;

        public string Duration
            => DurationConverter.GetDuration(BeginTime, EndTime);
    }
}
