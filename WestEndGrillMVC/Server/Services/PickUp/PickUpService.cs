using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WestEndGrillMVC.Server.Data;
using WestEndGrillMVC.Shared.Models.PickUp;

namespace WestEndGrillMVC.Server.Services.PickUp
{
    public class PickUpService : IPickUpService
    {
        public async Task<bool> CreatePickUpAsync(PickUpCreate model)
        {
            var pickUpEntity = new PickUp

            {
                PickUpName = _userId, //owner/user
                PickUpId = model.PickUpId,
                TimeOfDay = model.TimeOfDay,
                PickUpName = model.PickUpName,
                GuestId = model.GuestId,
                DateTime = DateTimeOffset.Now,
                DateTime = model.GuestId
            };
            _context.PickUps.Add(pickUpEntity);
            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        public async Task<bool> DeletePickUpAsync(int pickUpId)
        {
            var entity = await _context.PickUps.FindAsync(pickUpId);
            if (entity?.PickUpName != _userId) //owner
                return false;

            _context.PickUps.Remove(entity);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<IEnumerable<PickUpListItem>> GetAllPickUpsAsync()
        {
            var pickUpQuery = _context
                .PickUps
                .Where(n => n.PickUpName == _userId)
                .Select(n =>
                    new PickUpListItem
                    {
                        TimeOfDay = n.TimeOfDay,
                        PickUpName = n.PickUpName,
                      
                    });
            return await pickUpQuery.ToListAsync();
        }

        public async Task<PickUpDetail> GetPickUpByIdAsync(int pickUpId)
        {
            var pickUpEntity = await _context
                .PickUps
                .Include(nameof(Order)) //Category
                .FirstOrDefaultAsync(n => n.PickUpId == pickUpId && n.PickUpName == _userId);//user
            if (pickUpEntity is null)
                return null;

            var detail = new PickUpDetail
            {
                PickUpId = pickUpEntity.GuestId,
                TimeOfDay = pickUpEntity.TimeOfDay,
                PickUpName = pickUpEntity.PickUpName,
                GuestId = pickUpEntity.GuestId,
            };

            return detail;
        }

        public void SetGuestId(int PickUpId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> UpdatePickUpAsync(PickUpEdit model)
        {
            if (model == null) return false;

            var entity = await _context.PickUps.FindAsync(model.PickUpId);

            if (entity?.PickUpName != _userId) return false; //user 

            entity.PickUpId = model.PickUpId;
            entity.TimeOfDay = model.TimeOfDay;
            entity.PickUpName = model.PickUpName;
            entity.GuestId = model.GuestId;

            return await _context.SaveChangesAsync() == 1;
        }

        private readonly ApplicationDbContext _context;

        public PickUpService(ApplicationDbContext context)
        {
            _context = context;
        }

        private string _userId;
        public void SetUserId(string userId) => _userId = userId;
    }
}
