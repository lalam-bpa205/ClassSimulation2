using ClassSimulation2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClassSimulation2.DAL.Context
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
        public DbSet<Member> Members { get; set; }
        public DbSet<Position>Positions { get; set; }
    }
}
