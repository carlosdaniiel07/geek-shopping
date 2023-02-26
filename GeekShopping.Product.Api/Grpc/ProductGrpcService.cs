using AutoMapper;
using GeekShopping.Product.Api.Repository;
using GeekShopping.Shared.Protos;
using Grpc.Core;
using ProductModel = GeekShopping.Shared.Protos.Product;

namespace GeekShopping.Product.Api.Grpc
{
    public class ProductGrpcService : ProductService.ProductServiceBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductGrpcService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public override async Task GetAllProductsById(GetAllProductsByIdRequest request, IServerStreamWriter<ProductModel> responseStream, ServerCallContext context)
        {
            var ids = request.Id.Select(id => Guid.Parse(id)).ToList();
            var products = _mapper.Map<IEnumerable<ProductModel>>(await _productRepository.GetAllByIdAsync(ids));

            foreach (var product in products)
                await responseStream.WriteAsync(product);
        }
    }
}
