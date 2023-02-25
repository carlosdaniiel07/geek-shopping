namespace GeekShopping.Order.Api.Models.Dto
{
    public class AddOrderDto
    {
        public string Customer { get; set; }
        public string Document { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Coupon { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }

        public class OrderItem
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string Description { get; set; }
            public int Quantity { get; set; }
            public Guid ProductId { get; set; }
        }
    }
}
