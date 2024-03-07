using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FrontEndAAUH.Data {
    public class ApplicationDbContext : IdentityDbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }
    }
}
