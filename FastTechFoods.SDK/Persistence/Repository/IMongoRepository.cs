﻿using System.Linq.Expressions;

namespace FastTechFoods.SDK.Persistence.Repository
{
    public interface IMongoRepository<T> where T : class
    {
        Task<T> GetByIdAsync(string id);
        Task<List<T>> GetListAsync(List<string> ids);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task UpdateAsync(string id, T entity);
        Task DeleteAsync(string id);
    }
}