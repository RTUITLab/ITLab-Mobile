using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ITLab_Mobile.Api.Models.Extensions.Events;
using Models.PublicAPI.Responses.Event;
using EventTypeView = ITLab_Mobile.Api.Models.Event.EventType.EventTypeView;
using ITLab_Mobile.Api.Models.Event;

namespace ITLab_Mobile.Api
{
    public interface IEventApi
    {
        [Get("/api/Event")]
        Task<List<CompactEventViewExtended>> GetEvents();

        [Get("/api/Event/{eventId}")]
        Task<EventViewExtended> GetOneEvent(Guid eventId);

        [Get("/api/EventRole")]
        Task<List<EventRoleView>> GetEventRoles();

        [Post("/api/Event/wish/{placeId}/{roleId}")]
        Task<ApiResponse<string>> SendWishAsync(Guid placeId, Guid roleId);

        [Get("/api/EventType")]
        Task<List<EventTypeView>> GetEventTypes(string match);

        [Post("/api/Event")]
        Task<EventViewCreated> CreateEvent([Body] EventCreateRequest createRequest);
    }
}
