using OfficeBooker.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeBooker.Services.IServices
{
    public interface IOfficeService
    {
        Task<IEnumerable<OfficeDTO>> GetAllOfficesAsync();
        Task<OfficeDTO> CreateOfficeAsync(OfficeDTO officeDto);
    }
}
