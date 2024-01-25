using Application.Features.AuthFeature;
using Application.Features.RoleFeature;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var response = await _mediator.Send(new GetRoleListQuery());
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("sync")]
        public async Task<IActionResult> SyncRoles()
        {
            var response = await _mediator.Send(new SyncRoleCommand());
            return StatusCode(response.StatusCode, response);
        }
    }
}
