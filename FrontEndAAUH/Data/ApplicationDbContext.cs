using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FrontEndAAUH.Models;

namespace FrontEndAAUH.Data {
    public class ApplicationDbContext : IdentityDbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }
        public DbSet<FrontEndAAUH.Models.ProjectRole> ProjectRole { get; set; } = default!;
    }
}
