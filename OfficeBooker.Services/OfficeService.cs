using FluentValidation;
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
        private readonly IValidator<OfficeCreateDTO> _validator;
        public OfficeService(IUnitOfWork unitOfWork , IValidator<OfficeCreateDTO> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }
        public async Task<OfficeDTO> CreateOfficeAsync(OfficeCreateDTO officeDTO)
        {
            var office = officeDTO.Adapt<Office>();
            var validationResult = await _validator.ValidateAsync(officeDTO);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
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