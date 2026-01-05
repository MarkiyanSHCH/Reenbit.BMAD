using Reenbit.BMAD.Core.Internal.ServiceBus.Models;
using Reenbit.BMAD.Domain.Result;

namespace Reenbit.BMAD.Core.Internal.ServiceBus
{
    public interface IServiceBusPublisher
    {
        Task<IResult> SendMessageAsync(EventCreatedEvent @event);
    }
}
