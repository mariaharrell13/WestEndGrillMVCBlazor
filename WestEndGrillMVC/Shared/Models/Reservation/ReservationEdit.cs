using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WestEndGrillMVC.Shared.Models.Reservation
{
    public class ReservationEdit
    {
        [Required]
        public int ReservationId { get; set; }
        [Required]
        public int NumberOfGuests { get; set; }
        [Required]
        public DateTime TimeOfDay { get; set; }
        [Required]
        public string PartyName { get; set; }
        public int GuestId { get; set; }
    }
}
