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
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Office>().HasData(
                 // Floor 0
                new Office { OfficeNumber = 1, Capacity = 10, FloorNumber = 0 },
                new Office { OfficeNumber = 2, Capacity = 8, FloorNumber = 0 },
                new Office { OfficeNumber = 3, Capacity = 12, FloorNumber = 0 },

                // Floor 1
                new Office { OfficeNumber = 4, Capacity = 10, FloorNumber = 1 },
                new Office { OfficeNumber = 5, Capacity = 15, FloorNumber = 1 },
                new Office { OfficeNumber = 6, Capacity = 10, FloorNumber = 1 },
                new Office { OfficeNumber = 7, Capacity = 5, FloorNumber = 1 },

                // Floor 2
                new Office { OfficeNumber = 8, Capacity = 20, FloorNumber = 2 },
                new Office { OfficeNumber = 9, Capacity = 10, FloorNumber = 2 },
                new Office { OfficeNumber = 10, Capacity = 8, FloorNumber = 2 }
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
