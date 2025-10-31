using System.Linq.Expressions;

namespace CassianoStore.Domain.Abstractions;

public abstract class Specification<T> : ISpecification<T>
{
    public abstract Expression<Func<T, bool>> ToExpresstion();

    public bool IsSatisfiedBy(T entity)
    {
        var predicate = ToExpresstion().Compile();
        return predicate(entity);
    }
}
