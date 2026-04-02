using Microsoft.AspNetCore.Mvc;
using OfficeBooker.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeBooker.Services.IServices
{
    public interface IOfficeService
    {
        public Task<IEnumerable<OfficeDTO>> GetAllOfficesAsync();
        public Task<OfficeDTO> CreateOfficeAsync(OfficeCreateDTO officeDto);
        public Task<OfficeDTO> GetOfficeByIdAsync(int id);
        public Task DeleteOfficeAsync(int id);

    }
}
