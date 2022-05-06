using System.Collections.Generic;
using System.Threading.Tasks;
using WestEndGrillMVC.Shared.Models.Reservation;

namespace WestEndGrillMVC.Server.Services.Reservation
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationListItem>> GetAllReservationsAsync();
        Task<bool> CreateReservationAsync(ReservationCreate model);
        Task<ReservationDetail> GetReservationByIdAsync(int guestId);
        Task<bool> UpdateReservationAsync(ReservationEdit model);
        Task<bool> DeleteReservationAsync(int guestId);
        //Task<bool> DeleteReservationAsync(int GuestId);
        void SetGuestId(int GuestId);
    }
}
