using Microsoft.AspNetCore.Mvc;
using Reenbit.BMAD.API.Controllers.Events.Models;
using Reenbit.BMAD.Core.Entities.Events;
using Reenbit.BMAD.Domain.Entities;
using Reenbit.BMAD.Domain.Result;
using IResult = Reenbit.BMAD.Domain.Result.IResult;

namespace Reenbit.BMAD.API.Controllers.Events
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventsBoundary _boundary;

        public EventsController(IEventsBoundary boundary)
            => _boundary = boundary;

        /// <summary>
        /// Retrieves events.
        /// </summary>
        /// <returns>
        /// Returns <see cref="IActionResult"/> containing the events if found,
        /// or a <see cref="ProblemDetails"/> response if the request is unprocessable.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IResult<IEnumerable<Event>> result = await _boundary.GetAsync();

            if (result.IsFailed)
                return base.UnprocessableEntity(new ProblemDetails { Title = result.Error.Message });

            return base.Ok(new { result.Data });
        }

        /// <summary>
        /// Insert new event
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// 200 OK. <br/>
        /// 422 Unprocessable Entity with problem details on failure.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostEventRequest request)
        {
            IResult result = await _boundary.PublishEventAsync(request.UserId, request.Type, request.Description);

            if (result.IsFailed)
                return base.UnprocessableEntity(new ProblemDetails { Title = result.Error.Message });

            return base.Ok();
        }
    }
}
