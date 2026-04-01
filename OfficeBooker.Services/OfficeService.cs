using Mapster;
using OfficeBooker.DataAccess.Repository;
using OfficeBooker.DataAccess.Repository.I_Repository;
using OfficeBooker.Models;
using OfficeBooker.Models.DTOs;
using OfficeBooker.Services.IServices;


namespace OfficeBooker.Services
{
    public class OfficeService : IOfficeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OfficeService(IUnitOfWork unitOfWork)
        {
         _unitOfWork = unitOfWork;
        }
        public async Task<OfficeDTO> CreateOfficeAsync(OfficeDTO officeDTO)
        {
            var office = officeDTO.Adapt<Office>();

            _unitOfWork.officeRepository.Add(office);
            await _unitOfWork.Save();
            return office.Adapt<OfficeDTO>();
        }

        public async Task<IEnumerable<OfficeDTO>> GetAllOfficesAsync()
        {
            var offices = await _unitOfWork.officeRepository.GetAll();

            return offices.Adapt<IEnumerable<OfficeDTO>>();

        }
    }
}