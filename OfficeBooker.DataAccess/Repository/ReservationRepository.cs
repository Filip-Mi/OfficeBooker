using OfficeBooker.DataAccess.Data;
using OfficeBooker.DataAccess.Repository.I_Repository;
using OfficeBooker.Models;
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
            bool hasCollision = _db.Reservations.Any(existing =>
                existing.OfficeId == reservation.OfficeId &&
                reservation.ReservationStartTime < existing.ReservationEndTime &&
                existing.ReservationStartTime < reservation.ReservationEndTime);

            return !hasCollision;
        }


    }
}
