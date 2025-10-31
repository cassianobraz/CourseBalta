using System.Linq.Expressions;

namespace CassianoStore.Domain.Abstractions;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> ToExpresstion();
    bool IsSatisfiedBy(T entity);
}
