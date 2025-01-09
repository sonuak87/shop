using System;
using System.Linq.Expressions;
using Core.Interface;

namespace Core.Specification;

public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
{
    protected BaseSpecification() : this(null) { }
    public Expression<Func<T, bool>>? Criteria => criteria;

    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    public bool Distinct { get; private set; }

    protected void ApplyDistinct()
    {
        Distinct = true;
    }

    protected void AddOrderBy(Expression<Func<T, object>>? orderByExpression)
    {
        OrderBy = orderByExpression;
    }
    protected void AddOrderByDecending(Expression<Func<T, object>>? orderByDecendingExpression)
    {
        OrderByDescending = orderByDecendingExpression;
    }
}
public class BaseSpecification<T, TResult>(Expression<Func<T, bool>>? criteria)
: BaseSpecification<T>(criteria), ISpecification<T, TResult>
{

    protected BaseSpecification() : this(null) { }
    public Expression<Func<T, TResult>>? Select { get; private set; }

    protected void AddSelect(Expression<Func<T, TResult>>? selectExpression)
    {
        Select = selectExpression;
    }
}
