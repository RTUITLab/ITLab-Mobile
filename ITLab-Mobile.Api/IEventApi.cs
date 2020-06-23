using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models.PublicAPI.Responses.Event;

namespace ITLab_Mobile.Api
{
    public interface IEventApi
    {
        [Get("/api/Event")]
        Task<List<CompactEventView>> GetEvents();
    }
}
