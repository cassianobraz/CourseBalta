using CassianoStore.Domain.Abstractions;

namespace CassianoStore.Domain.Entities;

public class Product : Entity, IAggregateRoot
{
    public string Title { get; set; } = string.Empty;
}
