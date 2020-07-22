using ITLab_Mobile.Api.Models.User;
using ITLab_Mobile.Api.Models.User.Properties;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITLab_Mobile.Api
{
    public interface IUserApi
    {
        [Get("/api/User/{id}")]
        Task<UserView> GetUser(Guid id);

        [Get("/api/account/property")]
        Task<List<UserPropertyView>> GetUserProperties();

        [Get("/api/account/property/type")]
        Task<List<UserPropertyTypeView>> GetUserPropertyTypes();

        [Get("/api/account/property/type/{id}")]
        Task<UserPropertyTypeView> GetUserPropertyType(Guid id);

        [Put("/api/account")]
        Task<UserView> EditUserInfo([Body] UserInfoEditRequest userInfoEditRequest);
    }
}
