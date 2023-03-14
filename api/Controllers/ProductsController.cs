using Microsoft.AspNetCore.Mvc;
using core.Entities;
using core.Interfaces;
using core.Specifications;
using api.Dtos;
using AutoMapper;

namespace api.Controllers;


public class ProductsController : BaseApiController
{

    private readonly IGenericRepository<Product> _productRepo;
    private readonly IGenericRepository<ProductBrand> _brandRepo;
    private readonly IGenericRepository<ProductType> _typeRepo;
    private readonly IMapper _mapper;

    public ProductsController(IGenericRepository<Product> productRepo, IGenericRepository<ProductBrand> brandRepo, IGenericRepository<ProductType> typeRepo,
    IMapper mapper)
    {
        _productRepo = productRepo;
        _brandRepo = brandRepo;
        _typeRepo = typeRepo;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
    {
        var spec = new ProductsWithTypesAndBrandsSpecification();
        var products = await _productRepo.ListAsync(spec);

        return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));
    }
    // [HttpGet]
    // public async Task<ActionResult<Product>> GetProducts() => Ok(await _productRepo.ListAsync(new ProductsWithTypesAndBrandsSpecification()));


    // [HttpGet("{id}")]
    // public async Task<ActionResult<Product>> GetProduct(int id) => Ok(await _productRepo.GetEntityWithSpec(new ProductsWithTypesAndBrandsSpecification(id)));
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        var product = await _productRepo.GetEntityWithSpec(new ProductsWithTypesAndBrandsSpecification(id));
        return  _mapper.Map<Product,ProductToReturnDto>(product);
    }
    [HttpGet("brands")]
    public async Task<ActionResult<ProductBrand>> GetProductBrands() => Ok(await _brandRepo.ListAsync(new ProductBrandsSpecification()));

    [HttpGet("types")]
    public async Task<ActionResult<ProductType>> GetProductTypes() => Ok(await _typeRepo.ListAsync(new ProductTypeSpecification()));

}
