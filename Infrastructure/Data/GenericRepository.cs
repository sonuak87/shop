using System;
using Core.Entities;
using Core.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class GenericRepository<T>(StoreContext context) : IGenericRepository<T> where T : BaseEntity
{
    public void Add(T entity)
    {
        context.Set<T>().Add(entity);
    }

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        var query = context.Set<T>().AsQueryable();
        query = spec.ApplyCriteria(query);
        return await query.CountAsync();
    }

    public void Delete(T entity)
    {
        context.Set<T>().Remove(entity);
    }

    public bool Exists(int id)
    {
        return context.Set<T>().Any(x => x.Id == id);
    }

    public async Task<IReadOnlyList<T>> GetAllListAsync()
    {
        return await context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    public async Task<T?> GetEntityWithSpec(ISpecification<T> spec)
    {
        return await ApplySpecifications(spec).FirstOrDefaultAsync();
    }

    public async Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T, TResult> spec)
    {
        return await ApplySpecifications(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
    {
        return await ApplySpecifications(spec).ToListAsync();
    }

    public async Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> spec)
    {
        return await ApplySpecifications(spec).ToListAsync();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void Update(T entity)
    {
        context.Set<T>().Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
    }
    private IQueryable<T> ApplySpecifications(ISpecification<T> spec)
    {
        return SpecificationEvaluater<T>.GetQuery(context.Set<T>().AsQueryable(), spec);
    }
    private IQueryable<TResult> ApplySpecifications<TResult>(ISpecification<T, TResult> spec)
    {
        return SpecificationEvaluater<T>.GetQuery<T, TResult>(context.Set<T>().AsQueryable(), spec);
    }
}
