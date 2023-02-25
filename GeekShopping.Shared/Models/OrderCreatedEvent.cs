namespace GeekShopping.Shared.Models
{
    public class OrderCreatedEvent : BaseEvent
    {
        public Guid Id { get; set; }
        public string Customer { get; set; }
        public string Document { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public Guid UserId { get; set; }
        public string Coupon { get; set; }
        public decimal Total { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }

        public class OrderItem
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string Description { get; set; }
            public int Quantity { get; set; }
            public Guid ProductId { get; set; }
            public decimal Total { get; set; }
        }
    }
}
