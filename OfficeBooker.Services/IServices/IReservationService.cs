using Microsoft.AspNetCore.Mvc;
using OfficeBooker.Models.DTOs;

namespace OfficeBooker.Services.IServices
{
    public interface IReservationService
    {
        Task<ReservationCreateDTO> CreateReservationAsync(ReservationCreateDTO reservationCreateDTO, string userId);
        Task<IEnumerable<ReservationRespondDTO>> GetMyReservationsAsync(string userId);
    }
}
