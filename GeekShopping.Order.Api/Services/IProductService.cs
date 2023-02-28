using ProductModel = GeekShopping.Shared.Protos.Product;

namespace GeekShopping.Order.Api.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> GetAllByIdAsync(IEnumerable<Guid> ids);
    }
}
