using Reenbit.BMAD.Domain.Entities;
using Reenbit.BMAD.Domain.Result;

namespace Reenbit.BMAD.Core.Entities.Events
{
    public interface IEventsGateway
    {
        Task<IResult> CreateAsync(string userId, EventType type, string description);
        Task<IResult<IEnumerable<Event>>> GetAsync();
    }
}
