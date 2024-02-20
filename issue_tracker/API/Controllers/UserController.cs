using Application.Features.UserFeature;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {

        /// <summary>
        /// get all  users
        /// </summary>
        /// <returns>200</returns>

        [HttpGet]
        public async Task<IActionResult> Gets([FromQuery] GetAllUserQuery query)
        {
            var response = await _mediator.Send(query);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// User
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]        
        public async Task<IActionResult> Create([FromBody] CreateUserCommand dto)
        {
            var response = await _mediator.Send(dto);
            return StatusCode(response.StatusCode, response);
        }


        /// <summary>
        /// update user
        /// </summary>
        /// <returns>200</returns>
        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(int userId, UpdateCommand command)
        {
            command.UserId = userId;
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// delete user
        /// </summary>
        /// <returns>200</returns>
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(int userId)
        {
            var response = await _mediator.Send(new DeleteCommand() { UserId = userId });
            return StatusCode(response.StatusCode, response);
        }

    }
}
