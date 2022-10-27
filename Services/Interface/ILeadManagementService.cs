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
    public interface ILeadManagementService
    {
        Task<bool> AddLead(LeadManagmentRequestModel request);
        Task<bool> UpdateLead(LeadManagmentUpdateRequestModel request);
        Task<List<LeadManagmentResponseModel>> GetAllLeads();
        Task<LeadManagmentResponseModel?> GetLeadById(int? id);
        Task<bool> DeleteLead(int? id);
    }
}
