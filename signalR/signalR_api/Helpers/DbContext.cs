using Microsoft.EntityFrameworkCore;
using signalR_api.Models;

namespace signalR_api.Helpers
{
    public class MyAppContext : DbContext
    {
        public MyAppContext()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer(
            //    @"Server=(localdb)\mssqllocaldb;Database=Blogging;Integrated Security=True");
        }
    }
}