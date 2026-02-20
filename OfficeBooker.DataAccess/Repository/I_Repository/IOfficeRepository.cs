using OfficeBooker.cs;
using OfficeBooker.DataAccess.Repository.IRepository;


namespace OfficeBooker.DataAccess.Repository.I_Repository
{
    public interface IOfficeRepository : IRepository<Office>
    {
        void Update(Office entity);
    }
}
