using Microsoft.EntityFrameworkCore;
using RepositoryStore.Data;
using RepositoryStore.Models;
using RepositoryStore.Repository.Abstractions;

namespace RepositoryStore.Repository;

public class ProductRepository(AppDbContext context) : Repository<Product>(context), IProductRepository;
