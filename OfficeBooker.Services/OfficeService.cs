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
        public async Task<OfficeDTO> CreateOfficeAsync(OfficeCreateDTO officeDTO)
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
        public async Task<OfficeDTO> GetOfficeByIdAsync(int id)
        {
            var office = await _unitOfWork.officeRepository.Get(o => o.Id == id);
            if (office == null)
            {
                throw new KeyNotFoundException($"Office with ID {id} not found.");
            }
            return office.Adapt<OfficeDTO>();
        }
        public async Task DeleteOfficeAsync(int id)
        {
            var office = await _unitOfWork.officeRepository.Get(o => o.Id == id);
            if (office == null)
            {
                throw new KeyNotFoundException($"Office with ID {id} not found.");
            }
            _unitOfWork.officeRepository.Remove(office);
            await  _unitOfWork.Save();
        }
    }
}