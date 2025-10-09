using Microsoft.EntityFrameworkCore;
using Todo.Models;

namespace Todo.Data;

public class AppDbContext : DbContext
{
    public DbSet<TodoModel> Todos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source=app.db;Cache=Shared");
    }
}
