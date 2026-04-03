using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OfficeBooker.Models;

public class ApplicationDbContext : IdentityDbContext<Worker>
{
    public DbSet<Office> Offices { get; set; }
  
    public DbSet<Reservation> Reservations { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // --- 1. SEED ROLES ---
        string adminRoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210";
        string userRoleId = "3d6f185f-4c1f-557g-97bg-594e67ge8321";

 
            modelBuilder.Entity<IdentityRole>().HasData(
        new IdentityRole
        {
        Id = adminRoleId,
        Name = "Admin",
        NormalizedName = "ADMIN",
        ConcurrencyStamp = "c1c1c1c1-c1c1-c1c1-c1c1-c1c1c1c1c1c1" 
        },
        new IdentityRole
        {
        Id = userRoleId,
        Name = "User",
        NormalizedName = "USER",
        ConcurrencyStamp = "d2d2d2d2-d2d2-d2d2-d2d2-d2d2d2d2d2d2" 
        }

        );

        // --- 2. SEED USERS ---
        string adminUserId = "a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d";
        string regularUserId = "b2c3d4e5-f6a7-5b6c-9d0e-1f2a3b4c5d6e";

        modelBuilder.Entity<Worker>().HasData(
            new Worker
            {
                Id = adminUserId,
                UserName = "admin@office.com",
                NormalizedUserName = "ADMIN@OFFICE.COM",
                Email = "admin@office.com",
                NormalizedEmail = "ADMIN@OFFICE.COM",
                Name = "Adam",
                Surname = "Kowalski",
                EmailConfirmed = true,
                SecurityStamp = "f3b3b3b3-b3b3-b3b3-b3b3-b3b3b3b3b3b3",
                ConcurrencyStamp = "f3b3b3b3-b3b3-b3b3-b3b3-b3b3b3b3b3b3", 
                PasswordHash = "AQAAAAIAAYagAAAAEPP97ZR1UNP6eqlqngYOreeuul1yi+fhEx7/xbByV3NfG2peefpyJ4+6NtxTDpZHUA=="
            },
            new Worker
            {
                Id = regularUserId,
                UserName = "user@office.com",
                NormalizedUserName = "USER@OFFICE.COM",
                Email = "user@office.com",
                NormalizedEmail = "USER@OFFICE.COM",
                EmailConfirmed = true,
                Name = "Jan",
                Surname = "Nowak",
                SecurityStamp = "e2a2a2a2-a2a2-a2a2-a2a2-a2a2a2a2a2a2",
                ConcurrencyStamp = "e2a2a2a2-a2a2-a2a2-a2a2-a2a2a2a2a2a2", 
                PasswordHash = "AQAAAAIAAYagAAAAEJrVvgVKx5jIXVP3zOwt0qKDbhMbzFVf3VsEf6FdzF4EvEHbmsqEz4Vs7KF164+Rcw=="
            }
        );

        // --- 3. MAP USERS TO ROLES ---
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { RoleId = adminRoleId, UserId = adminUserId },
            new IdentityUserRole<string> { RoleId = userRoleId, UserId = regularUserId }
        );

        // --- 4. SEED OFFICES ---
        modelBuilder.Entity<Office>().HasData(
            new Office { Id = 1, OfficeNumber = 101, FloorNumber = 1, Capacity = 4, Equipment = "Monitor, Whiteboard" },
            new Office { Id = 2, OfficeNumber = 102, FloorNumber = 1, Capacity = 2, Equipment = "Dual Monitor" },
            new Office { Id = 3, OfficeNumber = 201, FloorNumber = 2, Capacity = 6, Equipment = "Projector, Conference Mic" },
            new Office { Id = 4, OfficeNumber = 301, FloorNumber = 3, Capacity = 1, Equipment = "Standing Desk" }
        );
    }
}