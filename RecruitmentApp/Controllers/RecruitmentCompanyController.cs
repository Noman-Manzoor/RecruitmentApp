
using Microsoft.AspNetCore.Mvc;
using Services.BusinessModels.RequestModel;
using Services.BusinessModels.UpdateModel;
using Services.Interface;

namespace RecruitmentApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecruitmentCompanyController : ControllerBase
    {
        private readonly IRecruitmentCompanyService _recruitmentCompanyService; 
        public RecruitmentCompanyController(IRecruitmentCompanyService recruitmentCompanyService)
        {
            _recruitmentCompanyService = recruitmentCompanyService;
        }

        [HttpPost]
        [Route("add-recruitment-company")]
        public async Task<IActionResult> AddRecruitmentCompany([FromBody] RecruitmentCompanyRequestModel request)
        {
            try
            {
                if (request != null)
                {
                    var recruitmentCompanyAdded = await this._recruitmentCompanyService.AddRecruitmentCompany(request);
                    return Ok(recruitmentCompanyAdded);
                }
                return Ok("Request payload cannot be null or empty");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut]
        [Route("update-recruitment-company")]
        public async Task<IActionResult> UpdateCompanyAsync([FromBody] RecruitmentCompanyUpdateRequestModel request)
        {
            try
            {
                if (request != null && request.RecruitmentCompanyId > 0)
                {
                    var companyUpdated = await this._recruitmentCompanyService.UpdateRecruitmentCompany(request);
                    return Ok(companyUpdated);
                }
                return Ok("Please enter the valid id");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("get-all-recruitment-companies")]
        public async Task<IActionResult> GetAllCompaniesAsync()
        {
            try
            {
                var companiesList = await this._recruitmentCompanyService.GetAllRecruitmentCompanies();
                return Ok(companiesList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("get-recruitment-company-by-id")]
        public async Task<IActionResult> GetCompanyByIdAsync(int? id)
        {
            try
            {
                if (id != null && id > 0)
                {
                    var company = await this._recruitmentCompanyService.GetRecruitmentCompanyById(id);
                    if (company.RecruitmentCompanyId != 0 && company != null)
                        return Ok(company);
                }
                return Ok("No Record Found");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete]
        [Route("delete-recruitment-company")]
        public async Task<IActionResult> DeleteRecruitmentCompany(int? id)
        {
            try
            {
                if (id != null && id > 0)
                {
                    var isCompanyDeleted = await this._recruitmentCompanyService.DeleteRecruitmentCompany(id);
                    return Ok(isCompanyDeleted);
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
