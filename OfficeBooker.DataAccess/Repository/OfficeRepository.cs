using OfficeBooker.DataAccess.Repository.I_Repository;
using OfficeBooker.Models;


namespace OfficeBooker.DataAccess.Repository
{
    public class OfficeRepository : Repository<Office>, IOfficeRepository
    {
        ApplicationDbContext _db;
        public OfficeRepository(ApplicationDbContext db) :base(db){ 
            _db=db; 
        }
        public void Update(Office entity)
        {
            _db.Offices.Update(entity);
        }
    }
}
