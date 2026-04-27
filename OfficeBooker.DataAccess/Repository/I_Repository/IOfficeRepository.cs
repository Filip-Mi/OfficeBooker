using OfficeBooker.DataAccess.Repository.IRepository;
using OfficeBooker.Models;


namespace OfficeBooker.DataAccess.Repository.I_Repository
{
    public interface IOfficeRepository : IRepository<Office>
    {
        void Update(Office entity);
        bool DoesOfficeExist(int officeId);
    }
}
