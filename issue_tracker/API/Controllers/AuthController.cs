using API.Attributes;
using Application.Features.AuthFeature;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

public class AuthController : BaseController
{
    /// <summary>
    /// User Login
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] UserLoginQuery query)
    {
        var response = await _mediator.Send(query);
        return StatusCode(response.StatusCode, response);
    }

    /// <summary>
    /// decode jwt token
    /// </summary>
    /// <returns>200 | 401</returns>
    [JwtAuthorize]
    [HttpGet("decode")]
    public async Task<IActionResult> GetAuthPayload()
    {
        var response = await _mediator.Send(new GetAuthPayloadQuery());
        return StatusCode(response.StatusCode, response);
    }
}
