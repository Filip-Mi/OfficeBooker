using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OfficeBooker.Models;
using OfficeBooker.Models;

namespace OfficeBooker.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<Worker>
    {
        public DbSet<Office> Offices {  get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
