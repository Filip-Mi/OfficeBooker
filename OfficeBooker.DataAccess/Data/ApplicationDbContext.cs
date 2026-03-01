using Microsoft.EntityFrameworkCore;
using OfficeBooker.Models;
using OfficeBooker.Models.cs;

namespace OfficeBooker.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Office> Offices {  get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Office>().HasData(
                // Floor 0
                new Office { Id = 1, OfficeNumber = 101, Capacity = 10, FloorNumber = 0 },
                new Office { Id = 2, OfficeNumber = 102, Capacity = 8, FloorNumber = 0 },
                new Office { Id = 3, OfficeNumber = 103, Capacity = 12, FloorNumber = 0 },

                // Floor 1
                new Office { Id = 4, OfficeNumber = 201, Capacity = 10, FloorNumber = 1 },
                new Office { Id = 5, OfficeNumber = 202, Capacity = 15, FloorNumber = 1 },
                new Office { Id = 6, OfficeNumber = 203, Capacity = 10, FloorNumber = 1 },
                new Office { Id = 7, OfficeNumber = 204, Capacity = 5, FloorNumber = 1 },

                // Floor 2
                new Office { Id = 8, OfficeNumber = 301, Capacity = 20, FloorNumber = 2 },
                new Office { Id = 9, OfficeNumber = 302, Capacity = 10, FloorNumber = 2 },
                new Office { Id = 10, OfficeNumber = 303, Capacity = 8, FloorNumber = 2 }
            );
            modelBuilder.Entity<Worker>().HasData(
                new Worker { Id = 1, Name = "Piotr", Surname = "Nowak" },
                new Worker { Id = 2, Name = "Aneta", Surname = "Kowalska" },
                new Worker { Id = 3, Name = "Mateusz", Surname = "Wiśniewski" },
                new Worker { Id = 4, Name = "Karolina", Surname = "Wójcik" },
                new Worker { Id = 5, Name = "Jakub", Surname = "Kowalczyk" },
                new Worker { Id = 6, Name = "Małgorzata", Surname = "Kamińska" },
                new Worker { Id = 7, Name = "Wojciech", Surname = "Lewandowski" },
                new Worker { Id = 8, Name = "Katarzyna", Surname = "Zielińska" }
            );



        }
    }
}
