using Domain.BusinessModels.Request;
using Domain.BusinessModels.Response;
using Domain.BusinessModels.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IProfileManagementService
    {
        Task<bool> AddProfileAsync(ProfileManagementRequestModel request);
        Task<bool> UpdateProfileAsync(ProfileManagementUpdateRequestModel request);
        Task<List<ProfileManagementResponseModel>> GetAllProfileAsync();
        Task<ProfileManagementResponseModel?> GetProfileByIdAsync(int? id);
        Task<bool> DeleteProfileAsync(int? id);
        Task<ProfileManagementResponseModel?> GetProfileByNameAsync(string name);
    }
}
