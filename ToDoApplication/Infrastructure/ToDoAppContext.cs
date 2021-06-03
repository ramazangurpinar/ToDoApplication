using Microsoft.EntityFrameworkCore;
using ToDoApplication.Models;

namespace ToDoApplication.Infrastructure
{
    public class ToDoAppContext: DbContext
    {
        public ToDoAppContext(DbContextOptions<ToDoAppContext> options)
            :base(options)
        {

        }
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
