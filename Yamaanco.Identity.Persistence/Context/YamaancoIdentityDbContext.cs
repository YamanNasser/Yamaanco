using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Yamaanco.Infrastructure.EF.Identity.Persistence.Models;

namespace Yamaanco.Infrastructure.EF.Identity.Persistence.Context
{
    public class YamaancoIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public YamaancoIdentityDbContext(DbContextOptions<YamaancoIdentityDbContext> options)
            : base(options)
        {
        }

        protected YamaancoIdentityDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}