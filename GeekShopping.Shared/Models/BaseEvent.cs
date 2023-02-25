namespace GeekShopping.Shared.Models
{
    public abstract class BaseEvent
    {
        public Guid CorrelationId { get; } = Guid.NewGuid();
        public DateTime EventCreatedAt { get; } = DateTime.UtcNow;
    }
}
