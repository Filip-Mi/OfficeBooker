using OfficeBooker.DataAccess.Repository.I_Repository;
using OfficeBooker.Models;
using OfficeBooker.Models.DTOs;
using OfficeBooker.Services.IServices;

namespace OfficeBooker.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReservationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ReservationCreateDTO>> GetMyReservationsAsync(string userId)
        {
            var reservations = await _unitOfWork.reservationRepository.GetAll(
                filter: r => r.WorkerId == userId,
                includeProperties: "Office"
            );

            return (IEnumerable<ReservationCreateDTO>)reservations.Select(r => new ReservationRespondDTO
            {
                Id = r.Id,
                WorkerId = r.WorkerId,
                ReservationStartTime = r.ReservationStartTime,
                ReservationEndTime = r.ReservationEndTime,
                ReservationCreateTime = r.ReservationCreateTime,
                OfficeId = r.OfficeId,
                OfficeNumber = r.Office.OfficeNumber,
                FloorNumber = r.Office.FloorNumber    
            });
        }

        public async Task<ReservationRespondDTO> CreateReservationAsync(ReservationCreateDTO dto, string userId)
        {
            var office = await _unitOfWork.officeRepository.Get(o => o.Id == dto.OfficeId);
            if (office == null)
            {
                throw new Exception();
            }

            var overlappingReservation = await _unitOfWork.reservationRepository.Get(r =>
                r.OfficeId == dto.OfficeId &&
                r.ReservationStartTime < dto.ReservationEndTime &&
                r.ReservationEndTime > dto.ReservationStartTime
            );

            if (overlappingReservation != null)
            {
                throw new Exception();
            }

            var reservation = new Reservation
            {
                OfficeId = dto.OfficeId,
                WorkerId = userId,
                ReservationStartTime = dto.ReservationStartTime,
                ReservationEndTime = dto.ReservationEndTime,
                ReservationCreateTime = DateTime.Now
            };

            _unitOfWork.reservationRepository.Add(reservation);
            await _unitOfWork.Save();

            return new ReservationRespondDTO
            {
                Id = reservation.Id,
                WorkerId = reservation.WorkerId,
                ReservationStartTime = reservation.ReservationStartTime,
                ReservationEndTime = reservation.ReservationEndTime,
                ReservationCreateTime = reservation.ReservationCreateTime,
                OfficeId = office.Id,
                OfficeNumber = office.OfficeNumber,
                FloorNumber = office.FloorNumber
            };
        }
    }
}
