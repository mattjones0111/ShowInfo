namespace Api.Controllers
{
    using System.Threading.Tasks;
    using Contracts.V1;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Process.Features.Shows;

    [ApiController]
    [Route("shows")]
    public class ShowsController : ControllerBase
    {
        readonly IMediator _mediator;

        public ShowsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetShowsAsync(
            int pageNumber = 1,
            int pageSize = 20)
        {
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
