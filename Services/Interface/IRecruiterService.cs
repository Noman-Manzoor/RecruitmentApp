using Services.BusinessModels.Request;
using Services.BusinessModels.Response;
using Services.BusinessModels.Update;

namespace Services.Interface
{
    public interface IRecruiterService
    {
        Task<bool> AddRecruiter(RecruiterRequestModel request);
        Task<bool> UpdateRecruiter(RecruiterUpdateRequestModel request);
        Task<List<RecruiterResponseModel>> GetAllRecruiters();
        Task<RecruiterResponseModel> GetRecruiterById(int? id);
        Task<bool> DeleteRecruiter(int? id);

    }
}
