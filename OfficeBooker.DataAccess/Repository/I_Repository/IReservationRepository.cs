using OfficeBooker.DataAccess.Repository.IRepository;
using OfficeBooker.Models;
using OfficeBooker.Models.DTOs;

namespace OfficeBooker.DataAccess.Repository.I_Repository
{
    public interface IReservationRepository : IRepository<Models.Reservation>
    {
        Task<bool> IsReservationAvailable(Reservation reservation);
        Task Update(Reservation reservation);
    }
}
