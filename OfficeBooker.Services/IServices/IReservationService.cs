using Microsoft.AspNetCore.Mvc;
using OfficeBooker.Models.DTOs;

namespace OfficeBooker.Services.IServices
{
    public interface IReservationService
    {
        Task<ReservationRespondDTO> CreateReservationAsync(ReservationCreateDTO reservationCreateDTO, string userId);
        Task<IEnumerable<ReservationCreateDTO>> GetMyReservationsAsync(string userId);
    }
}
