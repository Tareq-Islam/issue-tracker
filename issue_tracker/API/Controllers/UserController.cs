using Application.Features.UserFeature;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        /// <summary>
        /// User Create
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]        
        public async Task<IActionResult> UserCreate([FromBody] CreateUserCommand dto)
        {
            var response = await _mediator.Send(dto);
            return StatusCode(response.StatusCode, response);
        }
    }
}
