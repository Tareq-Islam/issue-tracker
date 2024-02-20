using API.Attributes;
using Application.Features.CategoryFeature;
using Domain.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        [JwtAuthorize(RoleEnum.Admin, RoleEnum.Super_Admin)]
        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var response = await _mediator.Send(new GetListQuery());
            return StatusCode(response.StatusCode, response);
        }

       
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommand dto)
        {
            var response = await _mediator.Send(dto);
            return StatusCode(response.StatusCode, response);
        }


    
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCommand command)
        {
            command.Id = id;
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteCommand() { Id = id });
            return StatusCode(response.StatusCode, response);
        }

    }
}
