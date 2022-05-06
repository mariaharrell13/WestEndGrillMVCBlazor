using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;

namespace WestEndGrillMVC.Server.Models
{
    public class PickUp
    {
        [Key]
        public int PickUpId { get; set; }
        [Required]
        public DateTime TimeOfDay { get; set; }
        [Required]
        public string PickUpName { get; set; }
        [ForeignKey("Guest")]
        public int GuestId { get; set; }
    }
}
