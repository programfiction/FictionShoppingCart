using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using core.Entities;

namespace core.Specifications
{
    public class ProductBrandsSpecification : BaseSpecification<ProductBrand>
    {
        public ProductBrandsSpecification()
        {
        }

        public ProductBrandsSpecification(int id) : base(x=>x.Id==id)
        {
        }
    }
}