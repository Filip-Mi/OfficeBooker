using OfficeBooker.DataAccess.Data;
using OfficeBooker.DataAccess.Repository.I_Repository;
using OfficeBooker.Models.cs;

namespace OfficeBooker.DataAccess.Repository
{
    internal class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        ApplicationDbContext _db;
        
        public ReservationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool IsReservationAvailable(Reservation reservation)
        {
            bool IsStartTimeOk = true;
            bool IsEndTimeOk = true;
            IQueryable<Reservation> beginingTime = dbSet;
                beginingTime = (IQueryable<Reservation>)beginingTime.Where(res => res.OfficeNumber == reservation.OfficeNumber)
                .Where(res => res.ReservationStartTime.Date == reservation.ReservationStartTime.Date)
                .Where(res => res.ReservationStartTime <= reservation.ReservationStartTime && res.ReservationStartTime >= reservation.ReservationEndTime).ToList();

           

            return false;
        }

        
    }
}
