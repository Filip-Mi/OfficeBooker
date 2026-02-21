using OfficeBooker.DataAccess.Data;
using OfficeBooker.DataAccess.Repository.I_Repository;
using OfficeBooker.Models.cs;

namespace OfficeBooker.DataAccess.Repository
{
    public class WorkerRepository : Repository<Worker>, IWorkerRepository 
    {
        ApplicationDbContext _db;
        public WorkerRepository(ApplicationDbContext db) : base(db) { 
            _db = db;
        }
        public void Update(Worker worker)
        {
            throw new NotImplementedException();
        }
    }
}
