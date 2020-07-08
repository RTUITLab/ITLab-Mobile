using ITLab_Mobile.Api.Models.Helpers;
using Models.PublicAPI.Responses.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITLab_Mobile.Api.Models.Extensions.Events
{
    public class ShiftViewGrouped : List<PlaceView>
    {
        public Guid Id { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }

        public ShiftViewGrouped(List<PlaceView> placeViews) : base(placeViews) { }

        public string Duration
            => DurationConverter.GetDuration(BeginTime, EndTime, isShift: true);

        public bool IsDescription
            => !string.IsNullOrEmpty(Description);
    }
}
