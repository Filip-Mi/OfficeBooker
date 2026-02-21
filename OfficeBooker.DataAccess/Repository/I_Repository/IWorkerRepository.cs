using OfficeBooker.DataAccess.Repository.IRepository;
using OfficeBooker.Models.cs;


namespace OfficeBooker.DataAccess.Repository.I_Repository
{
    public  interface IWorkerRepository : IRepository<Worker>
    {
        void Update(Worker worker);
    }
}
