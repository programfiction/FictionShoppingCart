using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using core.Entities;

namespace core.Specifications;

public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
{
    public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams) : base(x =>
        (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
        (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
        (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
    )
    {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
        AddOrderBy(p => p.Name);
        AddPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);
        if (!string.IsNullOrEmpty(productParams.Sort))
        {
            switch (productParams.Sort)
            {
                case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break;

                case "priceDesc":
                    AddOrderByDesc(p => p.Price);
                    break;
                case "nameAsc":
                    AddOrderBy(p => p.Name);
                    break;

                case "nameDesc":
                    AddOrderByDesc(p => p.Name);
                    break;

                default:
                    AddOrderBy(p => p.Name);
                    break;
            }
        }
    }

    public ProductsWithTypesAndBrandsSpecification(int Id) : base(x => x.Id == Id)
    {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
    }

}
