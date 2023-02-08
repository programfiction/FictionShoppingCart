using Microsoft.AspNetCore.Mvc;
using core.Entities;
using core.Interfaces;

namespace api.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {

      private readonly IGenericRepository<Product> _productRepo;
      private readonly IGenericRepository<ProductBrand> _brandRepo;
      private readonly IGenericRepository<ProductType> _typeRepo;
        public ProductsController(IGenericRepository<Product> productRepo,IGenericRepository<ProductBrand> brandRepo,IGenericRepository<ProductType> typeRepo)
           { _productRepo =  productRepo;
            _brandRepo =  brandRepo;
            _typeRepo =  typeRepo;
           }
        [HttpGet]
        public async Task<ActionResult<Product>> GetProducts() => Ok(await _productRepo.ListAllAsync());


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id) => Ok(await _productRepo.GetByIdAsync(id));

        [HttpGet("brands")]
        public async Task<ActionResult<ProductBrand>> GetProductBrands() => Ok(await _brandRepo.ListAllAsync());
        [HttpGet("types")]
        public async Task<ActionResult<ProductType>> GetProductTypes() => Ok(await _typeRepo.ListAllAsync());

    }
