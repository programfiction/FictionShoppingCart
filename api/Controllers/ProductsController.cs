using Microsoft.AspNetCore.Mvc;
using core.Entities;
using core.Interfaces;
using core.Specifications;
using api.Dtos;
using AutoMapper;
using api.Errors;
using api.Helpers;

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
    public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
        var countSpec= new ProductWithFiltersWithCountSpecification(productParams);
        var totalItems= await _productRepo.CountAsync(countSpec);
               var products = await _productRepo.ListAsync(spec);
    var data= _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

        return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex,productParams.PageSize,totalItems,data));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        var product = await _productRepo.GetEntityWithSpec(new ProductsWithTypesAndBrandsSpecification(id));
        if (product == null) return NotFound(new ApiResponse(404));
        return _mapper.Map<Product, ProductToReturnDto>(product);
    }
    [HttpGet("brands")]
    public async Task<ActionResult<ProductBrand>> GetProductBrands() => Ok(await _brandRepo.ListAsync(new ProductBrandsSpecification()));

    [HttpGet("types")]
    public async Task<ActionResult<ProductType>> GetProductTypes() => Ok(await _typeRepo.ListAsync(new ProductTypeSpecification()));

}
