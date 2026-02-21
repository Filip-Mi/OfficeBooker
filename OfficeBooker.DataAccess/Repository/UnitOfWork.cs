using OfficeBooker.DataAccess.Data;
using OfficeBooker.DataAccess.Repository.I_Repository;
namespace OfficeBooker.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ApplicationDbContext _db;
        public OfficeRepository OfficeRepository {  get; set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            OfficeRepository = new OfficeRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();

        }
    }
}
