using Microsoft.AspNetCore.Mvc;
using Services.BusinessModels.RequestModel;
using Services.BusinessModels.UpdateModel;
using Services.Interface;

namespace RecruitmentApp.Controllers
{
   

    [ApiController]
    [Route("[controller]")]
    public class LeadManagementController : ControllerBase
    {
        private readonly ILeadManagementService _leadManagementService;
        public LeadManagementController(ILeadManagementService leadManagementService)
        {
            _leadManagementService = leadManagementService;
        }

        [HttpPost]
        [Route("add-lead")]
        public async Task<IActionResult> AddLead([FromBody] LeadManagmentRequestModel request)
        {
            try
            {
                if (request != null)
                {
                    var isLeadAdded = await this._leadManagementService.AddLead(request);
                    return Ok(isLeadAdded);
                }
                return Ok("Request payload cannot be null or empty");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut]
        [Route("update-lead")]
        public async Task<IActionResult> UpdateLead([FromBody] LeadManagmentUpdateRequestModel request)
        {
            try
            {
                if (request != null && request.Id > 0)
                {
                    var leadUpdated = await this._leadManagementService.UpdateLead(request);
                    return Ok(leadUpdated);
                }
                return Ok("Please enter the valid id");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("get-all-leads")]
        public async Task<IActionResult> GetAllLeads()
        {
            try
            {
                var leads = await this._leadManagementService.GetAllLeads();
                return Ok(leads);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("get-lead-by-id")]
        public async Task<IActionResult> GetLeadById(int? id)
        {
            try
            {
                if (id != null && id > 0)
                {
                    var lead = await this._leadManagementService.GetLeadById(id);
                    return Ok(lead);
                }
                return Ok("Please enter the valid lead id");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete]
        [Route("delete-lead")]
        public async Task<IActionResult> DeleteLead(int? id)
        {
            try
            {
                if (id != null && id > 0)
                {
                    var isLeadDeleted = await this._leadManagementService.DeleteLead(id);
                    return Ok(isLeadDeleted);
                }
                return Ok("Please enter the valid id");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
