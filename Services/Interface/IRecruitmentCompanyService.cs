using Domain.BusinessModels.RequestModel;
using Domain.BusinessModels.ResponseModel;
using Domain.BusinessModels.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
