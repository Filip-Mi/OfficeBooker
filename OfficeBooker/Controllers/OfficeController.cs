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

        /// <summary>
        /// Retrieves a comprehensive list of all offices and their capacities.
        /// </summary>
        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<IEnumerable<OfficeDTO>>> GetOffices()
        {
            var offices = await _officeService.GetAllOfficesAsync();
            var officesDTO = offices.Adapt<IEnumerable<OfficeDTO>>();
            return Ok(officesDTO);
        }

        /// <summary>
        /// Creates a new office record in the system. Restricted to users with the Admin role.
        /// </summary>
        /// <param name="officeDTO">The office details including number and maximum desk capacity.</param>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("create")]
        public async Task<IActionResult> CreateOffice([FromBody] OfficeCreateDTO officeDTO)
        {
            // Mała uwaga: adaptujesz OfficeCreateDTO na OfficeCreateDTO - upewnij się, 
            // czy serwis nie powinien przyjmować modelu 'Office'
            var office = officeDTO.Adapt<OfficeCreateDTO>();
            await _officeService.CreateOfficeAsync(office);

            return Ok(office);
        }

        /// <summary>
        /// Retrieves detailed information about a specific office by its unique identifier.
        /// </summary>
        /// <param name="id">The unique ID of the office.</param>
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetOfficeById(int id)
        {
            var office = await _officeService.GetOfficeByIdAsync(id);
            return Ok(office);
        }

        /// <summary>
        /// Permanently removes an office record from the database.
        /// </summary>
        /// <param name="id">The unique ID of the office to be deleted.</param>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteOffice(int id)
        {
            await _officeService.DeleteOfficeAsync(id);
            return NoContent();
        }
    }
}