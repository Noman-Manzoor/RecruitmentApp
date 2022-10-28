using Services.BusinessModels.RequestModel;
using Services.BusinessModels.ResponseModel;
using Services.BusinessModels.UpdateModel;

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
