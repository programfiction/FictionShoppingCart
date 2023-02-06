using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using core.Entities;

namespace api.Controllers
{
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _context;
        public ProductsController(StoreContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<List<Product>> GetProducts() => await _context.Products.ToListAsync();
       

        [HttpGet("{id}")]
        public async Task<Product> GetProduct(int id) =>await  _context.Products.FirstOrDefaultAsync(x=>x.Id==id);
       
    }
}