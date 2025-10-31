using CassianoStore.Domain.Abstractions;

namespace CassianoStore.Domain.Repositories;

public interface IRepository<T> where T : IAggregateRoot;
