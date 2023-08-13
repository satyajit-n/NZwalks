using Microsoft.EntityFrameworkCore;
using NZwalks.API.Models.Domain;

namespace NZwalks.API.Data
{
    public class CrudWithPostgres : DbContext
    {
        public CrudWithPostgres(DbContextOptions<CrudWithPostgres> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }
    }
}
