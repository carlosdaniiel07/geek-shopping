namespace GeekShopping.Cart.Api.Models.Dto
{
    public class GetCartDto
    {
        public IEnumerable<ItemDto> Items { get; set; }
        public string Coupon { get; set; }
        public decimal TotalValue => Items?.Sum(item => item.TotalValue) ?? default;

        public class ItemDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public decimal TotalValue => Quantity * Price;
        }
    }
}
