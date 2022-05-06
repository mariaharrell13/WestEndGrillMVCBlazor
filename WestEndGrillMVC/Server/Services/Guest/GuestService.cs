using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WestEndGrillMVC.Server.Data;
using WestEndGrillMVC.Shared.Models.Guest;
using WestEndGrillMVC.Server.Models;


namespace WestEndGrillMVC.Server.Services.Guest
{
    public class GuestService : IGuestService
    {
        public async Task<bool> CreateGuestAsync(GuestCreate model)
        {
            var guestEntity = new Guest
            {
                FirstName = _userId, //owner/user
                GuestId = model.GuestId,
                FistName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                CreatedUtc = DateTimeOffset.Now,
                
            };
            _context.Guests.Add(guestEntity);
            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }


        public async Task<bool> DeleteGuestAsync(int guestId)
        {
            var entity = await _context.Guests.FindAsync(guestId);
            if (entity?.FirstName != _userId) //owner
                return false;

            _context.Guests.Remove(entity);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<IEnumerable<GuestListItem>> GetAllGuestsAsync()
        {
            var guestQuery = _context
                .Guests
                .Where(n => n.FirstName == _userId) //user/Owner
                .Select(n =>
                    new GuestListItem
                    {
                        GuestId = n.GuestId,
                        FirstName = n.FirstName
                    });
            return await guestQuery.ToListAsync();
        }

        public async Task<GuestDetail> GetGuestByIdAsync(int guestId)
        {
            var guestEntity = await _context
                .Guests
                .Include(nameof(Reservation)) //Category
                .FirstOrDefaultAsync(n => n.GuestId == guestId && n.FirstName == _userId);//user
            if (guestEntity is null)
                return null;

            var detail = new GuestDetail
            {
                GuestId = guestEntity.GuestId,
                FirstName = guestEntity.FirstName,
                LastName = guestEntity.LastName,
                PhoneNumber = guestEntity.PhoneNumber
            };

            return detail;
        }

        public async Task<bool> UpdateGuestAsync(GuestEdit model)
        {
            if (model == null) return false;

            var entity = await _context.Guests.FindAsync(model.GuestId);

            if (entity?.FirstName != _userId) return false; //user 

            entity.GuestId = model.GuestId;
            entity.FirstName = model.FirstName;
            entity.LastName = model.Lastname;
            entity.PhoneNumber = model.PhoneNumber;

            return await _context.SaveChangesAsync() == 1;
        }

        private readonly ApplicationDbContext _context;

        public GuestService(ApplicationDbContext context)
        {
            _context = context;
        }
        private string _userId;

        public void SetUserId(string userId) => _userId = userId;
     }
}
