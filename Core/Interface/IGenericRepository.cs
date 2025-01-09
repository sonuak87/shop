using System;
using Core.Entities;

namespace Core.Interface;

public interface IGenericRepository<T> where T : BaseEntity
{

    Task<IReadOnlyList<T>> GetAllListAsync();
    Task<T?> GetEntityWithSpec(ISpecification<T> spec);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T, TResult> spec);
    Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> spec);
    Task<T?> GetByIdAsync(int id);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    bool Exists(int id);
    Task<bool> SaveChangesAsync();
}
