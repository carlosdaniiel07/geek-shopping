using AutoMapper;
using GeekShopping.Product.Api.Data.ValueObjects;
using GeekShopping.Product.Api.Repository;
using Microsoft.AspNetCore.Mvc;
using ProductEntity = GeekShopping.Product.Api.Models.Entities.Product;

namespace GeekShopping.Product.Api.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVO>>> Index()
        {
            var products = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ProductVO>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductVO>> Get(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(_mapper.Map<ProductVO>(product));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductVO request)
        {
            await _repository.SaveAsync(_mapper.Map<ProductEntity>(request));
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ProductVO request)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.CategoryName = request.CategoryName;
            product.ImageUrl = request.ImageUrl;

            await _repository.UpdateAsync(product);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            await _repository.DeleteAsync(product);

            return Ok();
        }
    }
}
