using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Identity;
using OfficeBooker.DataAccess.Repository.I_Repository;
using OfficeBooker.Models;
using OfficeBooker.Models.DTOs;
using OfficeBooker.Models.Exceptions;
using OfficeBooker.Services.IServices;

namespace OfficeBooker.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Worker> _userManager;
        private readonly IValidator<ReservationCreateDTO> _validator;
        
        public ReservationService(IUnitOfWork unitOfWork, UserManager<Worker> userManager , IValidator<ReservationCreateDTO> validator)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _validator = validator;
        }

        public async Task<IEnumerable<ReservationRespondDTO>> GetMyReservationsAsync(string userId)
        {
            var reservations = await _unitOfWork.reservationRepository.GetAll(
                filter: r => r.WorkerId == userId,
                includeProperties: "Office"

            );

           

            return reservations.Select(r =>
            {
                var response = r.Adapt<ReservationRespondDTO>();
                if(r.Office != null)
                {
                    response.OfficeNumber = r.Office.OfficeNumber;
                    response.FloorNumber = r.Office.FloorNumber;
                }
                return response;
            }
            );
        }

        public async Task<ReservationRespondDTO> CreateReservationAsync(ReservationCreateDTO dto, string userId)
        {
            var office = await _unitOfWork.officeRepository.Get(o => o.Id == dto.OfficeId);
            var validationResult = await _validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (office == null)
            {
                throw new KeyNotFoundException("Office not found.");
            }

            var reservationEntity = dto.Adapt<Reservation>();
            if (!await _unitOfWork.reservationRepository.IsReservationAvailable(reservationEntity))
            {
                throw new OfficeAlreadyReservedException(dto.OfficeId);
            }
            if (dto.ReservationStartTime >= dto.ReservationEndTime) {
                throw new InvalidReservationPeriodException(dto.ReservationStartTime, dto.ReservationEndTime);
            }

            var worker = await _userManager.FindByIdAsync(userId);
            if (worker == null)
            {
                throw new KeyNotFoundException("Worker not found.");
            }

            var reservation = dto.Adapt<Reservation>();
            reservation.WorkerId = userId;
            reservation.WorkerName = $"{worker.Name} {worker.Surname}";
            reservation.ReservationCreateTime = DateTime.Now;

            _unitOfWork.reservationRepository.Add(reservation);
            await _unitOfWork.Save();

            var response = reservation.Adapt<ReservationRespondDTO>();
            response.OfficeNumber = office.OfficeNumber;
            response.FloorNumber = office.FloorNumber;
            return response;
        }

        public async Task DeleteReservationAsync(int id, string currentUserId)
        {
            var reservation = await _unitOfWork.reservationRepository.Get(r => r.Id == id);
            if (reservation == null) {
                throw new KeyNotFoundException($"Reservation with ID {id} not found.");
            }

            if(reservation.WorkerId != currentUserId)
            {
                throw new UnauthorizedAccessException("You are not allowed to delete someone else's reservation.");
            }
            
            _unitOfWork.reservationRepository.Remove(reservation);
            await _unitOfWork.Save();

        }
        public async Task<ReservationRespondDTO> GetByIdAsync(int id)
        {
            var reservation = await _unitOfWork.reservationRepository.Get(
                filter: r => r.Id == id
                );


            if (reservation == null)
            {
                throw new ReservationNotFoundException(id); 
            }

            var response = reservation.Adapt<ReservationRespondDTO>();
            if (reservation.Office != null)
            {
                response.OfficeNumber = reservation.Office.OfficeNumber;
                response.FloorNumber = reservation.Office.FloorNumber;
            }
            return response;
        }

        public async Task UpdateReservationAsync(int id, ReservationUpdateDTO reservation, string currentUserId)
        {
            var reservationEntity = await _unitOfWork.reservationRepository.Get(r => r.Id == id);
            if (reservationEntity == null)
            {
                throw new KeyNotFoundException($"Reservation with ID {id} not found.");
            }

                reservationEntity!.OfficeId = reservation.OfficeId ?? reservationEntity.OfficeId; 
                reservationEntity.ReservationStartTime = reservation.ReservationStartTime ?? reservationEntity.ReservationStartTime;
                reservationEntity.ReservationEndTime = reservation.ReservationEndTime ?? reservationEntity.ReservationEndTime;

            if(!await _unitOfWork.reservationRepository.IsReservationAvailable(reservationEntity)) {
                throw new OfficeAlreadyReservedException(reservationEntity.OfficeId);
            }
            if (_unitOfWork.officeRepository.Get(u => u.Id == reservationEntity.OfficeId) == null)
            {
                
            }
            await _unitOfWork.reservationRepository.Update(reservationEntity!);
            await _unitOfWork.Save();
        }
    }
}
