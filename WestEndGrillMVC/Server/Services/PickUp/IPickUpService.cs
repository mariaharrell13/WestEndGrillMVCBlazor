using System.Collections.Generic;
using System.Threading.Tasks;
using WestEndGrillMVC.Shared.Models.PickUp;

namespace WestEndGrillMVC.Server.Services.PickUp
{
    public interface IPickUpService
    {
        Task<IEnumerable<PickUpListItem>> GetAllPickUpsAsync();
        Task<bool> CreatePickUpAsync(PickUpCreate model);
        Task<PickUpDetail> GetPickUpByIdAsync(int guestId);
        Task<bool> UpdatePickUpAsync(PickUpEdit model);
        Task<bool> DeletePickUpAsync(int guestId);
        //Task<bool> DeleteOrderAsync(int GuestId);
        void SetGuestId(int PickUpId);
    }
}
