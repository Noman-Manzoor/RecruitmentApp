using Services.BusinessModels.RequestModel;
using Services.BusinessModels.ResponseModel;
using Services.BusinessModels.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ICompanyService
    {
        Task<bool> AddCompany(CompanyRequestModel request);
        Task<bool> UpdateCompany(CompanyUpdateRequestModel request);
        Task<List<CompanyResponseModel>> GetAllCompanies();
        Task<CompanyResponseModel> GetCompanyById(int? id);
        Task<bool> DeleteCompany(int? id);
    }
}
