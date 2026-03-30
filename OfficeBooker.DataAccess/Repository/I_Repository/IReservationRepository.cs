using OfficeBooker.DataAccess.Repository.IRepository;
using OfficeBooker.Models;

namespace OfficeBooker.DataAccess.Repository.I_Repository
{
    public interface IReservationRepository : IRepository<Models.Reservation>
    {
        bool IsReservationAvailable(Reservation reservation);
    }
}
