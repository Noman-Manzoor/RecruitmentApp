using Microsoft.AspNetCore.Mvc;
using Services.BusinessModels.Request;
using Services.BusinessModels.Update;
using Services.Interface;

namespace RecruitmentApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecruiterController : ControllerBase
    {
        private readonly IRecruiterService _recruiterService;
        public RecruiterController(IRecruiterService recruiterService)
        {
            _recruiterService = recruiterService;
        }

        [HttpPost]
        [Route("add-recruiter")]
        public async Task<IActionResult> AddRecruiterAsync([FromBody] RecruiterRequestModel request)
        {
            if (request != null)
            {
                var isRecruiterAdded = await this._recruiterService.AddRecruiter(request);
                return Ok(isRecruiterAdded);
            }
            return Ok("Payload is empty");
        }

        [HttpPut]
        [Route("update-recruiter")]
        public async Task<IActionResult> UpdateRecruiter([FromBody] RecruiterUpdateRequestModel request)
        {
            if (request != null && request.Id > 0)
            {
                var recruiterUpdated = await this._recruiterService.UpdateRecruiter(request);
                return Ok(recruiterUpdated);
            }
            return Ok(false);
        }

        [HttpGet]
        [Route("get-all-recruiters")]
        public async Task<IActionResult> GetAllRecruitersAsync()
        {
            var recruiter = await this._recruiterService.GetAllRecruiters();
            return Ok(recruiter);
        }

        [HttpGet]
        [Route("get-recruiter-by-id")]
        public async Task<IActionResult> GetRecruiterByIdAsync(int? id)
        {
            if (id != null)
            {
                var recruiter = await this._recruiterService.GetRecruiterById(id);
                if (recruiter != null)
                    return Ok(recruiter);
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("delete-recruiter")]
        public async Task<IActionResult> DeleteRecruiterAsync(int? id)
        {
            if (id != null)
            {
                var recruiter = await this._recruiterService.DeleteRecruiter(id);
                return Ok(recruiter);
            }
            return Ok($"Invalid Id {id}");
        }
    }

}
