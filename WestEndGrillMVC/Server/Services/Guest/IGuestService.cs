using System.Collections.Generic;
using System.Threading.Tasks;
using WestEndGrillMVC.Shared.Models.Guest;

namespace WestEndGrillMVC.Server.Services.Guest
{
    public interface IGuestService
    {
        Task<IEnumerable<GuestListItem>> GetAllGuestsAsync();
        Task<bool> CreateGuestAsync(GuestCreate model);
        Task<GuestDetail> GetGuestByIdAsync(int guestId);
        Task<bool> UpdateGuestAsync(GuestEdit model);
        Task<bool> DeleteGuestAsync(int guestId);
    }
}
