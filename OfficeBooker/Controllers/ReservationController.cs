using Microsoft.AspNetCore.Mvc;
using OfficeBooker.DataAccess.Repository.I_Repository;

namespace OfficeBooker.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class ReservationController : ControllerBase
    {
        public readonly  IUnitOfWork _unitOfWork;
        public ReservationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
