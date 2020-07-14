using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ITLab_Mobile.Api.Models.Extensions.Events;
using Models.PublicAPI.Responses.Event;

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
    }
}
