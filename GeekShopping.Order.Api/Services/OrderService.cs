using AutoMapper;
using GeekShopping.Order.Api.Models.Dto;
using GeekShopping.Order.Api.Repository;
using GeekShopping.Shared.Exchanges;
using GeekShopping.Shared.Interfaces;
using GeekShopping.Shared.Models;
using OrderEntity = GeekShopping.Order.Api.Models.Entities.Order;

namespace GeekShopping.Order.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IMessageBus _messageBus;

        public OrderService(IOrderRepository orderRepository, IUserService userService, IProductService productService, IMapper mapper, IMessageBus messageBus)
        {
            _orderRepository = orderRepository;
            _userService = userService;
            _productService = productService;
            _mapper = mapper;
            _messageBus = messageBus;
        }

        public async Task<AddOrderResponseDto> CreateAsync(AddOrderDto addOrderDto)
        {
            var hasItems = addOrderDto.Items?.Any() ?? false;

            if (!hasItems)
                return new AddOrderResponseDto("O pedido precisa conter ao menos 1 (um) item");

            var order = _mapper.Map<OrderEntity>(addOrderDto);

            order.UserId = _userService.GetLoggedUserId();

            if (order.Total <= 0)
                return new AddOrderResponseDto("O valor do pedido não pode ser 0 (zero)");

            var products = await _productService.GetAllByIdAsync(order.Items.Select(orderItem => orderItem.ProductId));

            foreach (var orderItem in order.Items)
            {
                var product = products.FirstOrDefault(p => p.Id == orderItem.ProductId.ToString());

                if (product == null)
                    return new AddOrderResponseDto($"O produto {orderItem.Name} não foi encontrado");

                var hasDifferentPrice = (decimal)product.Price != orderItem.Price;

                if (hasDifferentPrice)
                    return new AddOrderResponseDto($"O preço do produto {orderItem.Name} foi alterado");
            }

            await _orderRepository.SaveAsync(order);
            await _messageBus.PublishAsync(ExchangesConfig.OrderCreated, _mapper.Map<OrderCreatedEvent>(order));

            return new AddOrderResponseDto(order.Id);
        }
    }
}
