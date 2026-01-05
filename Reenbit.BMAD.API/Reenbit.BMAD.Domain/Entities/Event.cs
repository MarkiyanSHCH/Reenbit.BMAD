namespace Reenbit.BMAD.Domain.Entities
{
    public class Event
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public EventType Type { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
