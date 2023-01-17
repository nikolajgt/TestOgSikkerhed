using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestOgSikkerhed.Models;

namespace TestOgSikkerhed.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Here we add the table we want in our DB
        public DbSet<User> Users { get; set; }
    }
}