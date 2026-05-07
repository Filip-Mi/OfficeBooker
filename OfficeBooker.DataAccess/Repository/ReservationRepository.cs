using Microsoft.EntityFrameworkCore;
using OfficeBooker.DataAccess.Repository.I_Repository;
using OfficeBooker.Models;
using OfficeBooker.Models.DTOs;
using System.Linq.Expressions;

namespace OfficeBooker.DataAccess.Repository
{
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        ApplicationDbContext _db;
        
        public ReservationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<bool> IsReservationAvailable(Reservation reservation)
        {
            bool hasCollision = await _db.Reservations.AnyAsync(existing =>
                existing.OfficeId == reservation.OfficeId &&
                reservation.ReservationStartTime < existing.ReservationEndTime &&
                existing.ReservationStartTime < reservation.ReservationEndTime);

            return !hasCollision;
        }

        public async Task Update(Reservation reservation)
        {
            _db.Update(reservation);
        }
    }
}
