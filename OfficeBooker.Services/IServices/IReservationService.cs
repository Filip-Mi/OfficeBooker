using Microsoft.AspNetCore.Mvc;
using OfficeBooker.Models.DTOs;

namespace OfficeBooker.Services.IServices
{
    public interface IReservationService
    {
        public Task<ReservationRespondDTO> CreateReservationAsync(ReservationCreateDTO reservationCreateDTO, string userId);
        public Task<IEnumerable<ReservationRespondDTO>> GetMyReservationsAsync(string userId);
    }
}
