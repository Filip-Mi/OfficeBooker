using OfficeBooker.DataAccess.Repository.I_Repository;
using OfficeBooker.Models.cs.DTOs;
using OfficeBooker.Services.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeBooker.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReservationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<ReservationCreateDTO> CreateReservationAsync(ReservationCreateDTO reservationCreateDTO, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReservationCreateDTO>> GetMyReservationsAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
