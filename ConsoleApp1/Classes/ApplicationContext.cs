using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.Classes
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Person> Employees { get; set; } = null!;
        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;

        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=TestedDb;Trusted_Connection=True;");
        }
    }
}
