using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WestEndGrillMVC.Server.Services.Guest;
using WestEndGrillMVC.Server.Services.Order;
using WestEndGrillMVC.Server.Services.PickUp;
using WestEndGrillMVC.Server.Services.Reservation;
using WestEndGrillMVC.Shared.Models.Guest;

namespace WestEndGrillMVC.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestsController : ControllerBase
    {
        private readonly IGuestService _guestService;

        public GuestsController(IGuestService guestService)
        {
            _guestService = guestService;
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
            _guestService.SetUserId(userId);
            return true;
        }

        [HttpGet]
        public async Task<List<GuestListItem>> Index()
        {
            if (!SetUserIdInService()) return new List<GuestListItem>();

            var guests = await _guestService.GetAllGuestsAsync();
            return guests.ToList();
        }
        [HttpGet]
        public async Task<IActionResult> Guest(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var guest = await _guestService.GetAllGuestByIdAsync(id);
            if (guest == null) return NotFound();
            return Ok(guest);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GuestCreate model)
        {
            if (model == null) return BadRequest();
            if (!SetUserIdInService()) return Unauthorized();

            bool wasSuccessful = await _guestService.CreateGuestAsync(model);

            if (wasSuccessful) return Ok();
            else return UnprocessableEntity();
        }
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, GuestEdit model)
        {
            if (!SetUserIdInService()) return Unauthorized();
            if (model == null || !ModelState.IsValid) return BadRequest();
            if (model.GuestId != id) return BadRequest();

            bool wasSuccessful = await _guestService.UpdateGuestAsync(model);

            if (wasSuccessful) return Ok();
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var guest = await _guestService.GetGuestByIdAsync(id);
            if (guest == null) return NotFound();

            bool wasSuccessful = await _guestService.DeleteGuestAsync(id);

            if (!wasSuccessful) return BadRequest();

            return Ok();
        }
    }
}