namespace GeekShopping.Cart.Api.Models.Dto
{
    public class AddCartItemDto
    {
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }

        public class ProductDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
        }
    }
}
