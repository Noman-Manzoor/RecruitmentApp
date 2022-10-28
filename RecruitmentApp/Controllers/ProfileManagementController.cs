using Microsoft.AspNetCore.Mvc;
using Services.BusinessModels.Request;
using Services.BusinessModels.Response;
using Services.BusinessModels.Update;
using Services.Interface;

namespace RecruitmentApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileManagementController : ControllerBase
    {
        private readonly IProfileManagementService _profileService;
        public ProfileManagementController(IProfileManagementService profile)
        {
            _profileService = profile;
        }

        [HttpPost]
        [Route("add-profile")]
        public async Task<IActionResult> AddProfileAsync([FromBody] ProfileManagementRequestModel request)
        {
            try
            {
                if (request != null)
                {
                    var profileAdded = await this._profileService.AddProfileAsync(request);
                    return Ok(profileAdded);
                }
                return Ok("Payload is empty");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut]
        [Route("update-profile")]
        public async Task<IActionResult> UpdateProfileAsync([FromBody] ProfileManagementUpdateRequestModel request)
        {
            try
            {
                if (request != null && request.Id > 0)
                {
                    var profileUpdated = await this._profileService.UpdateProfileAsync(request);
                    return Ok(profileUpdated);
                }
                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("get-all-profile")]
        public async Task<IActionResult> GetAllProfileAsync()
        {
            try
            {
                var profile = await this._profileService.GetAllProfileAsync();
                return Ok(profile);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("get-profile-by-id")]
        public async Task<IActionResult> GetProfileByIdAsync(int? id)
        {
            try
            {
                var profile = new ProfileManagementResponseModel();
                if (id != null && id > 0)
                {
                    profile = await this._profileService.GetProfileByIdAsync(id);
                    return Ok(profile);
                    //if (profile != null)
                    //    return Ok(profile);
                }
                return Ok($"No record found against Id {id}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpGet]
        [Route("get-profile-by-name")]
        public async Task<IActionResult> GetProfileNameIdAsync(string name)
        {
            if (name != null)
            {
                var profile = await this._profileService.GetProfileByNameAsync(name);
                return Ok(profile);
                //if (profile != null)
                //    return Ok(profile);
            }
            return Ok($"No record found with name {name}");
        }

        [HttpDelete]
        [Route("delete-profile")]
        public async Task<IActionResult> DeleteProfileAsync(int? id)
        {
            try
            {
                if (id != null && id > 0)
                {
                    var profile = await this._profileService.DeleteProfileAsync(id);
                    return Ok(profile);
                }
                return Ok($"Invalid Id {id}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
