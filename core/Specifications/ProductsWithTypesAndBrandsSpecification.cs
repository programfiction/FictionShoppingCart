using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using core.Entities;

namespace core.Specifications;

public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
{
    public ProductsWithTypesAndBrandsSpecification()
    {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
    }

    public ProductsWithTypesAndBrandsSpecification(int Id) : base(x => x.Id == Id)
    {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
    }

}
