using core.Entities;
namespace core.Specifications;

public class ProductWithFiltersWithCountSpecification  :BaseSpecification<Product>
{

    public ProductWithFiltersWithCountSpecification(ProductSpecParams productParams) : base(x =>
        (string.IsNullOrEmpty(productParams.Search)||x.Name.ToLower().Contains(productParams.Search)) &&
        (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
        (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
    )
    {
      
    }
}
