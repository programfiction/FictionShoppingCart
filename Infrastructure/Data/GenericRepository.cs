using System.Runtime.InteropServices;
using System.ComponentModel;
using core.Entities;
using core.Interfaces;
using core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly StoreContext _context;
    public GenericRepository(StoreContext context) => this._context = context;
    public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
    public async Task<IReadOnlyList<T>> ListAllAsync() => await _context.Set<T>().ToListAsync();
    public async Task<T> GetEntityWithSpec(ISpecifications<T> spec) => await ApplySpecification(spec).FirstOrDefaultAsync();
    public async Task<IReadOnlyList<T>> ListAsync(ISpecifications<T> spec) => await ApplySpecification(spec).ToListAsync();
    private IQueryable<T> ApplySpecification(ISpecifications<T> spec) => SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
}
