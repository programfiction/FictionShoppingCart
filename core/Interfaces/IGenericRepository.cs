using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.Entities;
using core.Specifications;

namespace core.Interfaces;

    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int Id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetEntityWithSpec(ISpecifications<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecifications<T> spec); 
    }
