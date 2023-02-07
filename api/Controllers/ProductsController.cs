using Microsoft.AspNetCore.Mvc;
using core.Entities;
using core.Interfaces;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly IProductRepository _repo;
        public ProductsController(IProductRepository pRepo) => this._repo = pRepo;


        [HttpGet]
        public async Task<ActionResult<Product>> GetProducts() => Ok(await _repo.GetProductsAsync());


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id) => Ok(await _repo.GetProductByIdAsync(id));

        [HttpGet("brands")]
        public async Task<ActionResult<ProductBrand>> GetProductBrands() => Ok(await _repo.GetProductBrandAsync());
        [HttpGet("types")]
        public async Task<ActionResult<ProductType>> GetProductTypes() => Ok(await _repo.GetProductTypeAsync());

    }
}