using API.Attributes;
using Application.Features.IssueFeature;
using Domain.Enums;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : BaseController
    {       
        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var response = await _mediator.Send(new GetListQuery());
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetStatus()
        {
            var response = await _mediator.Send(new GetIssueStatusCountQuery());
            return StatusCode(response.StatusCode, response);
        }

        //[HttpGet("{id}/track")]
        //public async Task<IActionResult> GetTrack(int id)
        //{
        //    var response = await _mediator.Send(new GetListQuery());
        //    return StatusCode(response.StatusCode, response);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] CreateCommand dto)
        //{
        //    var response = await _mediator.Send(dto);
        //    return StatusCode(response.StatusCode, response);
        //}

        //[HttpPost("{id}/comment")]
        //public async Task<IActionResult> CreateComment([FromBody] CreateCommand dto)
        //{
        //    var response = await _mediator.Send(dto);
        //    return StatusCode(response.StatusCode, response);
        //}

        //[HttpPut("{id}/close")]
        //public async Task<IActionResult> Close(int id)
        //{
        //    var response = await _mediator.Send(new GetListQuery());
        //    return StatusCode(response.StatusCode, response);
        //}

        //[HttpPut("{id}/suspend")]
        //public async Task<IActionResult> Suspend(int id)
        //{
        //    var response = await _mediator.Send(new GetListQuery());
        //    return StatusCode(response.StatusCode, response);
        //}

        //[HttpPut("{id}/causeFinding")]
        //public async Task<IActionResult> UpdateCauseFindings(int id)
        //{
        //    var response = await _mediator.Send(new GetListQuery());
        //    return StatusCode(response.StatusCode, response);
        //}

        //[HttpPut("{id}/solutionTag")]
        //public async Task<IActionResult> UpdateSolutionTag(int id)
        //{
        //    var response = await _mediator.Send(new GetListQuery());
        //    return StatusCode(response.StatusCode, response);
        //}

        //[HttpPut("{id}/Assignee")]
        //public async Task<IActionResult> UpdateAssignees(int id)
        //{
        //    var response = await _mediator.Send(new GetListQuery());
        //    return StatusCode(response.StatusCode, response);
        //}


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var response = await _mediator.Send(new GetListQuery());
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
