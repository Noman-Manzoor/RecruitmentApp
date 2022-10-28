using Services.BusinessModels.Request;
using Services.BusinessModels.Response;
using Services.BusinessModels.Update;

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
