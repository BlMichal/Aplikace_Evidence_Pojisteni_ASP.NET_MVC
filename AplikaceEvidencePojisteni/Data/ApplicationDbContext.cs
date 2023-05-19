using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AplikaceEvidencePojisteni.Models;

namespace AplikaceEvidencePojisteni.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AplikaceEvidencePojisteni.Models.User>? User { get; set; }
        public DbSet<AplikaceEvidencePojisteni.Models.Insurance>? Insurance { get; set; }
    }
}