using Microsoft.EntityFrameworkCore;
using OfficeBooker.Models;
using OfficeBooker.Models.cs;

namespace OfficeBooker.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Office> Offices {  get; set; }
        public DbSet<Worker> Workers { get; set; }
        DbSet<Reservation> Reservations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Office>().HasData(new Office { OfficeNumber = 1, Capacity = 10, FloorNumber = 0 });   
        }
    }
}
