using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WestEndGrillMVC.Server.Services.Order;
using WestEndGrillMVC.Shared.Models.Order;

namespace WestEndGrillMVC.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
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
            _orderService.SetUserId(userId);
            return true;
        }

        [HttpGet]
        public async Task<List<OrderListItem>> Index()
        {
            if (!SetUserIdInService()) return new List<OrderListItem>();

            var orders = await _orderService.GetAllOrdersAsync();
            return orders.ToList();
        }
        [HttpGet]
        public async Task<IActionResult> Order(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var order = await _orderService.GetAllOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderCreate model)
        {
            if (model == null) return BadRequest();
            if (!SetUserIdInService()) return Unauthorized();

            bool wasSuccessful = await _orderService.CreateOrderAsync(model);

            if (wasSuccessful) return Ok();
            else return UnprocessableEntity();
        }
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, OrderEdit model)
        {
            if (!SetUserIdInService()) return Unauthorized();
            if (model == null || !ModelState.IsValid) return BadRequest();
            if (model.OrderId != id) return BadRequest();

            bool wasSuccessful = await _orderService.UpdateOrderAsync(model);

            if (wasSuccessful) return Ok();
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();

            bool wasSuccessful = await _orderService.DeleteOrderAsync(id);

            if (!wasSuccessful) return BadRequest();

            return Ok();
        }
    }
}
