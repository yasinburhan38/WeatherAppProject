using AuthorizationApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationApi
{
    public class AuthorizationContext : DbContext
    {
        public AuthorizationContext(DbContextOptions<AuthorizationContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; } = null!;

    }
}
