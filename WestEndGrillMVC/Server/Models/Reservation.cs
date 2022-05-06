using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;

namespace WestEndGrillMVC.Server.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        [Required]
        public int NumberOfGuests { get; set; }
        [Required]
        public DateTime TimeOfDay { get; set; }
        [Required]
        public string PartyName { get; set; }
        [ForeignKey("Guest")]
        public int GuestId { get; set; }
    }
}
