using System;
using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specification;

//specification
public class BaseSpecification<T> : ISpecification<T>
{
    private readonly Expression<Func<T, bool>>? _criteria;

    protected BaseSpecification(Expression<Func<T, bool>>? criteria)
    {
        _criteria = criteria;
    }

    //default constructor that sets no filter
    protected BaseSpecification() : this(null) { }

    public Expression<Func<T, bool>>? Criteria => _criteria;

    public Expression<Func<T, object>>? OrderBy { get; private set; }

    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    public bool IsDistinct { get; private set; }

    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    protected void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByDescending = orderByDescExpression;
    }

    protected void ApplyDistinct()
    {
        IsDistinct = true;
    }


}

//projection
public class BaseSpecification<T, TResult> : BaseSpecification<T>, ISpecification<T, TResult>
{
    protected BaseSpecification(Expression<Func<T, bool>>? criteria) : base(criteria)
    {
    }
    protected BaseSpecification() : this(null) { }
    
    public Expression<Func<T, TResult>>? Select { get; private set; }

    protected void AddSelect(Expression<Func<T, TResult>> selectExpression)
    {
        Select = selectExpression;
    }
}
