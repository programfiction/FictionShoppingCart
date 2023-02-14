using Microsoft.AspNetCore.Mvc;
using core.Entities;
using core.Interfaces;
using core.Specifications;
using api.Dtos;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{

    private readonly IGenericRepository<Product> _productRepo;
    private readonly IGenericRepository<ProductBrand> _brandRepo;
    private readonly IGenericRepository<ProductType> _typeRepo;
    public ProductsController(IGenericRepository<Product> productRepo, IGenericRepository<ProductBrand> brandRepo, IGenericRepository<ProductType> typeRepo)
    {
        _productRepo = productRepo;
        _brandRepo = brandRepo;
        _typeRepo = typeRepo;
    }
    [HttpGet]
    public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts()
    {
        var spec = new ProductsWithTypesAndBrandsSpecification();
        var products = await _productRepo.ListAsync(spec);

        return Ok(products.Select(product => new ProductToReturnDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            PictureUrl = product.PictureUrl,
            Price = product.Price,
            ProductBrand = product.ProductBrand.Name,
            ProductType = product.ProductType.Name,
        }).ToList());
    }
    // [HttpGet]
    // public async Task<ActionResult<Product>> GetProducts() => Ok(await _productRepo.ListAsync(new ProductsWithTypesAndBrandsSpecification()));


    // [HttpGet("{id}")]
    // public async Task<ActionResult<Product>> GetProduct(int id) => Ok(await _productRepo.GetEntityWithSpec(new ProductsWithTypesAndBrandsSpecification(id)));
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        var product = await _productRepo.GetEntityWithSpec(new ProductsWithTypesAndBrandsSpecification(id));
        return new ProductToReturnDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            PictureUrl = product.PictureUrl,
            Price = product.Price,
            ProductBrand = product.ProductBrand.Name,
            ProductType = product.ProductType.Name,
        };
    }
    [HttpGet("brands")]
    public async Task<ActionResult<ProductBrand>> GetProductBrands() => Ok(await _brandRepo.ListAsync(new ProductBrandsSpecification()));

    [HttpGet("types")]
    public async Task<ActionResult<ProductType>> GetProductTypes() => Ok(await _typeRepo.ListAsync(new ProductTypeSpecification()));

}
