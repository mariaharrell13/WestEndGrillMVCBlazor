using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WestEndGrillMVC.Server.Services.Reservation;
using WestEndGrillMVC.Shared.Models.Reservation;

namespace WestEndGrillMVC.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }
        private string GetUserId()
        {
            string userIdClaim = User.Claims.First(i => i.Type == ClaimTypes.Nameidentifier).Value;
            if (userIdClaim == null) return null;
            return userIdClaim;
        }
        private bool SetUserIdInService()
        {
            var userId = GetUserId();
            if (userId == null) return false;
            _reservationService.SetUserId(userId);
            return true;
        }

        [HttpGet]
        public async Task<List<ReservationListItem>> Index()
        {
            if (!SetUserIdInService()) return new List<ReservationListItem>();

            var reservations = await _reservationService.GetAllReservationsAsync();
            return reservations.ToList();
        }
        [HttpGet]
        public async Task<IActionResult> Reservation(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var reservation = await _reservationService.GetAllReservationByIdAsync(id);
            if (reservation == null) return NotFound();
            return Ok(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReservationCreate model)
        {
            if (model == null) return BadRequest();
            if (!SetUserIdInService()) return Unauthorized();

            bool wasSuccessful = await _reservationService.CreateReservationAsync(model);

            if (wasSuccessful) return Ok();
            else return UnprocessableEntity();
        }
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, ReservationEdit model)
        {
            if (!SetUserIdInService()) return Unauthorized();
            if (model == null || !ModelState.IsValid) return BadRequest();
            if (model.ReservationId != id) return BadRequest();

            bool wasSuccessful = await _reservationService.UpdateReservationAsync(model);

            if (wasSuccessful) return Ok();
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null) return NotFound();

            bool wasSuccessful = await _reservationService.DeleteReservationAsync(id);

            if (!wasSuccessful) return BadRequest();

            return Ok();
        }
    }
}
