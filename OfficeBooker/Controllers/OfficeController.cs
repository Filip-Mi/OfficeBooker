using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeBooker.DataAccess.Repository.I_Repository;
using OfficeBooker.Models;
using OfficeBooker.Models.DTOs;

namespace OfficeBooker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OfficeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public OfficeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<IEnumerable<OfficeDTO>>> GetOffices()
        {
            var offices = await _unitOfWork.officeRepository.GetAll();

            var officesDTO = offices.Select(o => new OfficeDTO { Id = o.Id, OfficeNumber = o.OfficeNumber, Capacity = o.Capacity , FloorNumber = o.FloorNumber , Equipment = o.Equipment}).ToList();
            return Ok(officesDTO);
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]
        [Route("create")]
        public async Task<IActionResult> CreateOffice([FromBody]OfficeCreateDTO officeDTO)
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
            return Ok(office);

        }
       

    }
}
