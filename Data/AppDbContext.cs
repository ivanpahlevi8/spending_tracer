using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpendTracerApi.Models;

namespace SpendTracerApi.Data
{
    public class AppDbContext : IdentityDbContext<UserModel>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserModel> User { get; set; }

        public DbSet<SpendingModels> Spending { get; set; }
    }
}
