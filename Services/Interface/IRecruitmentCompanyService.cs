using Services.BusinessModels.RequestModel;
using Services.BusinessModels.ResponseModel;
using Services.BusinessModels.UpdateModel;

namespace Services.Interface
{
    public interface IRecruitmentCompanyService
    {
        Task<bool> AddRecruitmentCompany(RecruitmentCompanyRequestModel request);
        Task<bool> UpdateRecruitmentCompany(RecruitmentCompanyUpdateRequestModel request);
        Task<List<RecruitmentCompanyResponseModel>> GetAllRecruitmentCompanies();
       Task<RecruitmentCompanyResponseModel> GetRecruitmentCompanyById(int? id);
        Task<bool> DeleteRecruitmentCompany(int? id);
    }
}
