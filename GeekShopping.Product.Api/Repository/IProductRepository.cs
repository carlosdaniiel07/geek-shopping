using ProductEntity = GeekShopping.Product.Api.Models.Entities.Product;

namespace GeekShopping.Product.Api.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductEntity>> GetAllAsync();
        Task<IEnumerable<ProductEntity>> GetAllByIdAsync(IEnumerable<Guid> ids);
        Task<ProductEntity> GetByIdAsync(Guid id);
        Task SaveAsync(ProductEntity product);
        Task UpdateAsync(ProductEntity product);
        Task DeleteAsync(ProductEntity product);
    }
}
