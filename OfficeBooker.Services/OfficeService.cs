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
        public OfficeService(UnitOfWork unitOfWork)
        {
         _unitOfWork = unitOfWork;
        }
        public async Task<OfficeDTO> CreateOfficeAsync(OfficeDTO officeDTO)
        {
            var office = new Office
            {
                FloorNumber = officeDTO.FloorNumber,
                Capacity = officeDTO.Capacity,
                OfficeNumber = officeDTO.OfficeNumber,
                Equipment = officeDTO.Equipment
            };
            _unitOfWork.officeRepository.Add(office);
            await _unitOfWork.Save();
            officeDTO.Id = office.Id;
            return officeDTO;
        }

        public async Task<IEnumerable<OfficeDTO>> GetAllOfficesAsync()
        {
            var offices = await _unitOfWork.officeRepository.GetAll();

            return offices.Select(o => new OfficeDTO { Id = o.Id, OfficeNumber = o.OfficeNumber, Capacity = o.Capacity, FloorNumber = o.FloorNumber, Equipment = o.Equipment }).ToList();
             
        }
    }
}
