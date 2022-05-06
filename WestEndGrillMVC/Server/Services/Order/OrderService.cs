using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WestEndGrillMVC.Server.Data;
using WestEndGrillMVC.Shared.Models.Order;

namespace WestEndGrillMVC.Server.Services.Order
{
    public class OrderService : IOrderService
    {
        public async Task<bool> CreateOrderAsync(OrderCreate model)
        {
            var orderEntity = new Order

            {
                PickUpId = _userId, //owner/user
                OrderId = model.OrderId,
                Entree = model.Entree,
                Side = model.Side,
                Drink = model.Drink,
                Desert = model.Desert,
                SetPickUpId = model.PickUpId,
                DateTime = DateTimeOffset.Now,
                DateTime = model.OrderId
            };
            _context.Orders.Add(orderEntity);
            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            var entity = await _context.Orders.FindAsync(orderId);
            if (entity?.Entree != _userId) //owner
                return false;

            _context.Orders.Remove(entity);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<IEnumerable<OrderListItem>> GetAllOrdersAsync()
        {
            var orderQuery = _context
                .Orders
                .Where(n => n.Entree == _userId)
                .Select(n =>
                    new OrderListItem
                    {
                        Entree = n.Entree,
                        Side = n.Side,
                        Drink = n.Drink,
                        Desert = n.Desert,
                        PickUpId = n.PickUpId

                    });
            return await orderQuery.ToListAsync();
        }

        public async Task<OrderDetail> GetOrderByIdAsync(int orderId)
        {
            var orderEntity = await _context
                .Orders
                .Include(nameof(Guest)) //Category
                .FirstOrDefaultAsync(n => n.OrderId == orderId && n.Entree == _userId);//user
            if (orderEntity is null)
                return null;

            var detail = new OrderDetail
            {
                PickUpId = orderEntity.PickUpId,
                Entree = orderEntity.Entree,
                Side = orderEntity.Side,
                Drink = orderEntity.Drink,
                Desert = orderEntity.Desert,
                OrderId = orderEntity.OrderId,
            };

            return detail;
        }

        public void SetPickUpId(int PickUpId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> UpdateOrderAsync(OrderEdit model)
        {
            if (model == null) return false;

            var entity = await _context.Orders.FindAsync(model.PickUpId);

            if (entity?.Entree != _userId) return false; //user 

            entity.PickUpId = model.PickUpId;
            entity.OrderId = model.OrderId;
            entity.Entree = model.Entree;
            entity.Side = model.Side;
            entity.Drink = model.Drink;
            entity.Desert = model.Desert;
            entity.OrderId = model.OrderId;

            return await _context.SaveChangesAsync() == 1;
        }

        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        private string _userId;

        public void SetUserId(string userId) => _userId = userId;
    }
}
