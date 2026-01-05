using Reenbit.BMAD.Domain.Entities;

namespace Reenbit.BMAD.Sql.DataAccess.Events.Models
{
    internal class EventDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public int TypeId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public Event ToModel()
           => new()
           {
               Id = this.Id,
               UserId = this.UserId,
               Type = (EventType)TypeId,
               Description = this.Description,
               CreatedAt = this.CreatedAt
           };
    }
}
