using OfficeBooker.DataAccess.Repository.IRepository;
using OfficeBooker.Models.cs;

namespace OfficeBooker.DataAccess.Repository.I_Repository
{
    public interface IReservationRepository : IRepository<Models.cs.Reservation>
    {
        bool IsReservationAvailable(Reservation reservation);
    }
}
