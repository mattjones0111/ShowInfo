namespace Api.Controllers
{
    using System.Threading.Tasks;
    using Contracts.V1;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Process.Features.Shows;

    [ApiController]
    [Route("shows")]
    public class ShowController : ControllerBase
    {
        readonly IMediator _mediator;

        public ShowController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetShowsAsync(
            int pageNumber = 1,
            int pageSize = 20)
        {
            if (pageNumber < 1 ||
                pageSize < 1 ||
                pageSize > Constants.MaximumPageSize)
            {
                return BadRequest();
            }

            Show[] result = await _mediator.Send(
                new Get.Query
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                });

            return Ok(result);
        }
    }
}
