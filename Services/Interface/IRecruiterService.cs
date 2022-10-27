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
    public interface IRecruiterService
    {
        Task<bool> AddRecruiter(RecruiterRequestModel request);
        Task<bool> UpdateRecruiter(RecruiterUpdateRequestModel request);
        Task<List<RecruiterResponseModel>> GetAllRecruiters();
        Task<RecruiterResponseModel> GetRecruiterById(int? id);
        Task<bool> DeleteRecruiter(int? id);

    }
}
