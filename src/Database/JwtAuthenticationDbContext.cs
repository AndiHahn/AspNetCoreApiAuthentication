using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppAuthentication.Models;

namespace WebAppAuthentication.Database
{
    public class JwtAuthenticationDbContext : IdentityDbContext<AppUser>
    {
        public JwtAuthenticationDbContext(DbContextOptions<JwtAuthenticationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
