using Reenbit.BMAD.Core.Internal.ServiceBus;
using Reenbit.BMAD.Core.Internal.ServiceBus.Models;
using Reenbit.BMAD.Domain.Entities;
using Reenbit.BMAD.Domain.Result;

namespace Reenbit.BMAD.Core.Entities.Events
{
    public class EventsInteractor : IEventsBoundary
    {
        private readonly IEventsGateway _gateway;
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public EventsInteractor(IEventsGateway gateway, IServiceBusPublisher serviceBusPublisher)
            => (_gateway, _serviceBusPublisher) = (gateway, serviceBusPublisher);

        /// <summary>
        /// Retrieves events.
        /// </summary>
        /// <returns>
        /// An <see cref="IResult{T}"/> containing a collection of <see cref="Event"/> objects,
        /// or an error result if retrieval fails.
        /// </returns>
        public Task<IResult<IEnumerable<Event>>> GetAsync()
            => _gateway.GetAsync();

        /// <summary>
        /// Create the Event for a given user.
        /// </summary>
        /// <param name="userId">The userId.</param>
        /// <param name="type">The type.</param>
        /// <param name="description">The description.</param>
        /// <returns>
        /// An <see cref="IResult"/> indicating the success or failure of the update operation.
        /// </returns>
        public Task<IResult> InsertAsync(string userId, EventType type, string description)
            => _gateway.CreateAsync(userId, type, description);

        /// <summary>
        /// Publish event to queue.
        /// </summary>
        /// <param name="userId">The userId.</param>
        /// <param name="type">The type.</param>
        /// <param name="description">The description.</param>
        /// <returns>
        /// An <see cref="IResult"/> indicating the success or failure of the update operation.
        /// </returns>
        public Task<IResult> PublishEventAsync(string userId, EventType type, string description)
            => _serviceBusPublisher.SendMessageAsync(new EventCreatedEvent(userId, type, description));
    }
}
