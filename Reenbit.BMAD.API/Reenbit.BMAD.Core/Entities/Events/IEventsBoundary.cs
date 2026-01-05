using Reenbit.BMAD.Domain.Entities;
using Reenbit.BMAD.Domain.Result;

namespace Reenbit.BMAD.Core.Entities.Events
{
    public interface IEventsBoundary
    {
        Task<IResult> PublishEventAsync(string userId, EventType type, string description);
        Task<IResult<IEnumerable<Event>>> GetAsync();
        Task<IResult> InsertAsync(string userId, EventType type, string description);
    }
}
