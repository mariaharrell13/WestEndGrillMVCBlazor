using System.Collections.Generic;
using System.Threading.Tasks;
using WestEndGrillMVC.Shared.Models.Order;

namespace WestEndGrillMVC.Server.Services.Order
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderListItem>> GetAllOrdersAsync();
        Task<bool> CreateOrderAsync(OrderCreate model);
        Task<OrderDetail> GetOrderByIdAsync(int guestId);
        Task<bool> UpdateOrderAsync(OrderEdit model);
        Task<bool> DeleteOrderAsync(int guestId);
        //Task<bool> DeleteOrderAsync(int PickUpId);
        void SetPickUpId(int PickUpId);
    }
}
