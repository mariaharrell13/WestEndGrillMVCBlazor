using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WestEndGrillMVC.Server.Data;
using WestEndGrillMVC.Shared.Models.Reservation;

namespace WestEndGrillMVC.Server.Services.Reservation
{
    public class ReservationService : IReservationService
    {
        public async Task<bool> CreateReservationAsync(ReservationCreate model)
        {
            var reservationEntity = new Reservation

            {
                PartyName = _userId, //owner/user
                ReservationId = model.ReservationId,
                NumberOfGuests = model.NumberOfGuests,
                TimeOfDay = model.TimeOfDay,
                GuestId = model.GuestId,
                PartyName = model.PartyName,
                DateTime = DateTimeOffset.Now
            };
            _context.PickUps.Add(reservationEntity);
            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteReservationAsync(int reservationId)
        {
            var entity = await _context.Reservations.FindAsync(reservationId);
            if (entity?.PartyName != _userId) //owner
                return false;

            _context.Reservations.Remove(entity);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<IEnumerable<ReservationListItem>> GetAllReservationsAsync()
        {
            var reservationQuery = _context
                .Reservations
                .Where(n => n.PartyName == _userId)
                .Select(n =>
                    new ReservationListItem
                    {
                        TimeOfDay = n.TimeOfDay,
                        NumberOfGuests = n.NumberOfGuests,
                        PartyName = n.PartyName,

                    });
            return await reservationQuery.ToListAsync();
        }

        public async Task<ReservationDetail> GetReservationByIdAsync(int reservationId)
        {
            var reservationEntity = await _context
                .Reservations
                .Include(nameof(Guest)) //Category
                .FirstOrDefaultAsync(n => n.ReservationId == reservationId && n.PartyName == _userId);//user
            if (reservationEntity is null)
                return null;

            var detail = new ReservationDetail
            {
                ReservationId = reservationEntity.ReservationId,
                TimeOfDay = reservationEntity.TimeOfDay,
                NumberOfGuests = reservationEntity.NumberOfGuests,
                PartyName = reservationEntity.PartyName,
                GuestId = reservationEntity.GuestId,
            };

            return detail;
        }

        public void SetGuestId(int GuestId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> UpdateReservationAsync(ReservationEdit model)
        {
            if (model == null) return false;

            var entity = await _context.Reservations.FindAsync(model.ReservationId);

            if (entity?.PartyName != _userId) return false; //user 

            entity.ReservationId = model.ReservationId;
            //entity.TimeOfDay = DateTimeOffset.Now;
            entity.NumberOfGuests = model.NumberOfGuests;
            entity.GuestId = model.GuestId;
            entity.PartyName = model.PartyName;
            entity.TimeOfDay = model.TimeOfDay;

            return await _context.SaveChangesAsync() == 1;
        }
        private readonly ApplicationDbContext _context;

        public ReservationService(ApplicationDbContext context)
        {
            _context = context;
        }

        private string _userId;

        public void SetUserId(string userId) => _userId = userId;
    }
}
