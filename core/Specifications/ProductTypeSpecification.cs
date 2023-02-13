using core.Entities;

namespace core.Specifications;

    public class ProductTypeSpecification : BaseSpecification<ProductType>
    {
        public ProductTypeSpecification()
        {
        }

        public ProductTypeSpecification(int id) : base(x=>x.Id==id)
        {
        }
    }
