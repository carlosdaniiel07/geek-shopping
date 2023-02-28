using GeekShopping.Shared.Protos;
using Grpc.Core;
using Grpc.Net.Client;
using ProductGrpcService = GeekShopping.Shared.Protos.ProductService;
using ProductModel = GeekShopping.Shared.Protos.Product;

namespace GeekShopping.Order.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;

        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<ProductModel>> GetAllByIdAsync(IEnumerable<Guid> ids)
        {
            var products = new List<ProductModel>();
            var grpcAddress = _configuration.GetSection("ServicesUrl").GetValue<string>("ProductService");
            var channel = GrpcChannel.ForAddress(grpcAddress);
            var client = new ProductGrpcService.ProductServiceClient(channel);
            var request = new GetAllProductsByIdRequest();

            request.Id.AddRange(ids.Select(id => id.ToString()));

            using var call = client.GetAllProductsById(request);

            await foreach (var response in call.ResponseStream.ReadAllAsync())
                products.Add(response);

            return products;
        }
    }
}
