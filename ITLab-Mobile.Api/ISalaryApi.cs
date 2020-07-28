using ITLab_Mobile.Api.Models.Extensions.Salary;
using ITLab_Mobile.Api.Models.Salary;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITLab_Mobile.Api
{
    public interface ISalaryApi
    {
        [Get("/api/salary/v1/event")]
        Task<List<EventSalaryCompactViewExtended>> GetSalaries();

        [Get("/api/salary/v1/event/{eventId}")]
        Task<ApiResponse<EventSalaryFullViewExtended>> GetOneSalary(Guid eventId);

        [Put("/api/salary/v1/event/{eventId}")]
        Task<EventSalaryFullView> CreateEditSalary(Guid eventId, [Body] EventSalaryCreateEdit salaryCreateEdit);
    }
}
