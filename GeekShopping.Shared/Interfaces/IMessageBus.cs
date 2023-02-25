using GeekShopping.Shared.Models;

namespace GeekShopping.Shared.Interfaces
{
    public interface IMessageBus
    {
        Task PublishAsync<TEvent>(string exchange, TEvent data) where TEvent : BaseEvent;
    }
}
