using GeekShopping.Shared.Models;

namespace GeekShopping.Shared.Interfaces
{
    public interface IMessageBus
    {
        Task PublishAsync<TEvent>(string exchange, TEvent data) where TEvent : BaseEvent;
        void Subscribe<TEvent>(string queue, Func<TEvent, Task<bool>> handler) where TEvent : BaseEvent;
    }
}
