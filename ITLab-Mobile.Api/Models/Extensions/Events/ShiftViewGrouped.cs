using ITLab_Mobile.Api.Models.Helpers;
using Models.PublicAPI.Responses.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITLab_Mobile.Api.Models.Extensions.Events
{
    public class ShiftViewGrouped : List<PlaceViewExtended>
    {
        public Guid Id { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }

        public ShiftViewGrouped(List<PlaceView> placeViews)
        {
            int counter = 1;
            foreach(var place in placeViews)
            {
                base.Add(new PlaceViewExtended
                {
                    Id = place.Id,
                    Description = place.Description,
                    TargetParticipantsCount = place.TargetParticipantsCount,
                    PlaceCount = $"Место #{counter} |",
                    Equipment = place.Equipment,
                    Participants = place.Participants,
                    Invited = place.Invited,
                    Wishers = place.Wishers,
                    Unknowns = place.Unknowns
                });
                counter++;
            }
        }

        public string Duration
            => DurationConverter.GetDuration(BeginTime, EndTime, isShift: true);

        public bool IsDescription
            => !string.IsNullOrEmpty(Description);
    }
}
