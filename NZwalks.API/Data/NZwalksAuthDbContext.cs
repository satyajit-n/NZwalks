using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZwalks.API.Data
{
    public class NZwalksAuthDbContext : IdentityDbContext
    {
        public NZwalksAuthDbContext(DbContextOptions<NZwalksAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "531d72b9-c97b-4401-b32f-7385565ec273";
            var writerRoleId = "8a31ff4f-98fd-41e1-8717-5c76afa80d42";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
