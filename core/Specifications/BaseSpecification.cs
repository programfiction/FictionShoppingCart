using System;
using System.Linq.Expressions;

namespace core.Specifications;

public class BaseSpecification<T> : ISpecifications<T>
{
    public BaseSpecification()
    {
    }

    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<T, bool>> Criteria { get; }

    public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();


    protected void AddInclude(Expression<Func<T, object>> includesExpression) => Includes.Add(includesExpression);
}
