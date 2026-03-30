using OfficeBooker.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeBooker.Services.IServices
{
    public interface IOfficeService
    {
        public Task<IEnumerable<OfficeDTO>> GetAllOfficesAsync();
        public Task<OfficeDTO> CreateOfficeAsync(OfficeDTO officeDto);
    }
}
