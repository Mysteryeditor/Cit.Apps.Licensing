﻿using Cit.Apps.Licensing.Application.ReadModels;
using Cit.Apps.Shared.Result;

namespace Cit.Apps.Licensing.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> Entities { get; }

        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

  
    }
}
