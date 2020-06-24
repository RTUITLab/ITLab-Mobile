using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models.PublicAPI.Responses.Event;
using ITLab_Mobile.Api.Models.Extensions;

namespace ITLab_Mobile.Api
{
    public interface IEventApi
    {
        [Get("/api/Event")]
        Task<List<CompactEventViewExtended>> GetEvents();
    }
}
