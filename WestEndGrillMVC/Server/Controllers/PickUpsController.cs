using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WestEndGrillMVC.Server.Services.PickUp;
using WestEndGrillMVC.Shared.Models.PickUp;

namespace WestEndGrillMVC.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PickUpsController : ControllerBase
    {
        private readonly IPickUpService _pickUpService;

        public PickUpsController(IPickUpService pickUpService)
        {
            _pickUpService = pickUpService;
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
            _pickUpService.SetUserId(userId);
            return true;
        }

        [HttpGet]
        public async Task<List<PickUpListItem>> Index()
        {
            if (!SetUserIdInService()) return new List<PickUpListItem>();

            var pickUps = await _pickUpService.GetAllPickUpsAsync();
            return pickUps.ToList();
        }
        [HttpGet]
        public async Task<IActionResult> PickUp(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var pickUp = await _pickUpService.GetAllPickUpByIdAsync(id);
            if (pickUp == null) return NotFound();
            return Ok(pickUp);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PickUpCreate model)
        {
            if (model == null) return BadRequest();
            if (!SetUserIdInService()) return Unauthorized();

            bool wasSuccessful = await _pickUpService.CreatePickUpAsync(model);

            if (wasSuccessful) return Ok();
            else return UnprocessableEntity();
        }
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, PickUpEdit model)
        {
            if (!SetUserIdInService()) return Unauthorized();
            if (model == null || !ModelState.IsValid) return BadRequest();
            if (model.PickUpId != id) return BadRequest();

            bool wasSuccessful = await _pickUpService.UpdatePickUpAsync(model);

            if (wasSuccessful) return Ok();
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var pickUp = await _pickUpService.GetPickUpByIdAsync(id);
            if (pickUp == null) return NotFound();

            bool wasSuccessful = await _pickUpService.DeletePickUpAsync(id);

            if (!wasSuccessful) return BadRequest();

            return Ok();
        }
    }
}
