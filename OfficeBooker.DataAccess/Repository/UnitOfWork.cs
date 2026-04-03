using OfficeBooker.DataAccess.Repository.I_Repository;
namespace OfficeBooker.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ApplicationDbContext _db;
        public IReservationRepository reservationRepository { get; private set; }

        public IOfficeRepository officeRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            officeRepository = new OfficeRepository(_db);
            reservationRepository = new ReservationRepository(_db);
        }
        public Task Save()
        {
            return  _db.SaveChangesAsync();
        }
    }
}
