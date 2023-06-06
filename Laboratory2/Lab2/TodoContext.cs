using Lab2.Services;
using Microsoft.EntityFrameworkCore;

namespace Lab2;

public class TodoContext : DbContext
{
    public DbSet<ToDoItem> ExpenseBaseModels { get; set; }

    public TodoContext(DbContextOptions options) : base(options)
    {
        
    }

    public TodoContext()
    {
        
    }
}