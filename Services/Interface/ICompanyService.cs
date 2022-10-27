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
    public interface ICompanyService
    {
        Task<bool> AddCompanyAsync(CompanyRequestModel request);
        Task<bool> UpdateCompanyAsync(CompanyUpdateRequestModel request);
        Task<List<CompanyResponseModel>> GetAllCompaniesAsync();
        Task<CompanyResponseModel> GetCompanyByIdAsync(int? id);
        Task<bool> DeleteCompanyAsync(int? id);
    }
}
