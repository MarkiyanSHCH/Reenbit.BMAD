using Reenbit.BMAD.Domain.Entities;

namespace Reenbit.BMAD.Core.Internal.ServiceBus.Models
{
    public class EventCreatedEvent
    {
        public EventCreatedEvent(
            string userId,
            EventType type,
            string description)
        {
            UserId = userId;
            Type = type;
            Description = description;
        }

        public string UserId { get; set; }
        public EventType Type { get; set; }
        public string Description { get; set; }
    }
}
