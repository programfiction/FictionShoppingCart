
using core.Entities;
using core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context) => this._context = context;

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync() => await _context.ProductBrands.ToListAsync();

        public async Task<Product> GetProductByIdAsync(int id) => await _context.Products
        .Include(p => p.ProductType)
        .Include(p => p.ProductBrand)
        .FirstOrDefaultAsync(x => x.Id == id);
        public async Task<IReadOnlyList<Product>> GetProductsAsync() => await _context.Products
        .Include(p => p.ProductType)
        .Include(p => p.ProductBrand)
        .ToListAsync();

        public async Task<IReadOnlyList<ProductType>> GetProductTypeAsync() => await _context.ProductTypes.ToListAsync();
    }
