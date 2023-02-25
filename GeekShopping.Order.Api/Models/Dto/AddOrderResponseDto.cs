namespace GeekShopping.Order.Api.Models.Dto
{
    public class AddOrderResponseDto
    {
        public string Error { get; private set; }
        public Order Data { get; private set; }
        public bool Success => string.IsNullOrWhiteSpace(Error);

        public AddOrderResponseDto(string error)
        {
            Error = error;
        }

        public AddOrderResponseDto(Guid orderId)
        {
            Data = new Order
            {
                Id = orderId,
            };
        }

        public class Order
        {
            public Guid Id { get; set; }
        }
    }
}
