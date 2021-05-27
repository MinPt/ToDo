using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDo.Data.Entities;

namespace ToDo.Data
{
    public class ToDoDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<ToDoItem> ToDoItems { get; set; }

        public ToDoDbContext(DbContextOptions<ToDoDbContext> option) : base(option)
        {
        }
    }
}