using Microsoft.AspNetCore.Mvc;
using Services.BusinessModels.RequestModel;
using Services.BusinessModels.UpdateModel;
using Services.Interface;

namespace RecruitmentApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost]
        [Route("add-company")]
        public async Task<IActionResult> AddCompanyAsync([FromBody] CompanyRequestModel request)
        {
            try
            {
                if (request != null)
                {
                    var companyAdded = await this._companyService.AddCompany(request);
                    return Ok(companyAdded);
                }
                return Ok("Request payload cannot be null or empty");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut]
        [Route("update-company")]
        public async Task<IActionResult> UpdateCompanyAsync([FromBody] CompanyUpdateRequestModel request)
        {
            try
            {
                if (request != null && request.Id > 0)
                {
                    var companyUpdated = await this._companyService.UpdateCompany(request);
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
        [Route("get-all-companies")]
        public async Task<IActionResult> GetAllCompaniesAsync()
        {
            try
            {
                var company = await this._companyService.GetAllCompanies();
                return Ok(company);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("get-company-by-id")]
        public async Task<IActionResult> GetCompanyByIdAsync(int? id)
        {
            try
            {
                if (id != null && id > 0)
                {
                    var company = await this._companyService.GetCompanyById(id);
                    if (company.CompanyId != 0 && company != null)
                        return Ok(company);
                }
                return Ok("Please enter the valid company id");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete]
        [Route("delete-company")]
        public async Task<IActionResult> DeleteCompanyAsync(int? id)
        {
            try
            {
                if (id != null && id > 0)
                {
                    var company = await this._companyService.DeleteCompany(id);
                    return Ok(company);
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
