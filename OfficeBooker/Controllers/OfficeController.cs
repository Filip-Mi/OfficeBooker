using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeBooker.DataAccess.Repository.I_Repository;
using OfficeBooker.Models;
using OfficeBooker.Models.DTOs;
using OfficeBooker.Services.IServices;

namespace OfficeBooker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OfficeController : ControllerBase
    {
        private readonly IOfficeService _officeService;

        public OfficeController(IOfficeService officeService)
        {
            _officeService = officeService;
        }
        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<IEnumerable<OfficeDTO>>> GetOffices()
        {
            var offices = await _officeService.GetAllOfficesAsync();

            var officesDTO = offices.Adapt<OfficeDTO>();
            return Ok(officesDTO);
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]
        [Route("create")]
        public async Task<IActionResult> CreateOffice([FromBody]OfficeCreateDTO officeDTO)
        {
            var office = officeDTO.Adapt<OfficeCreateDTO>();
            await _officeService.CreateOfficeAsync(office);
            
            return Ok(office);
        } 
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetOfficeById(int id)
        {
            var office = _officeService.GetOfficeByIdAsync(id);
            return Ok(office);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteOffice(int id)
        {
            await _officeService.DeleteOfficeAsync(id);
            return NoContent(); 
        }

    }
}
