using Microsoft.EntityFrameworkCore;
using MyFirstEFCoreApp.Entity;

namespace MyFirstEFCoreApp
{
    internal class EFCoreDBContext : DbContext
    {
        private const string ConnectionString =
        @"Server=(localdb)\mssqllocaldb;
             Database=MyFirstEFCoreDB;
             Trusted_Connection=True";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        public DbSet<Book> Books { get; set; }
    }
}
