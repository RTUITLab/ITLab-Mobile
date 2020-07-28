using ITLab_Mobile.Api.Models.Equipment;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ITLab_Mobile.Api
{
    public interface IEquipmentApi
    {
        // equipment user
        [Get("/api/equipment/user/{userId}")]
        Task<List<EquipmentView>> GetUserEquipment(Guid userId);
    }
}
