using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WestEndGrillMVC.Shared.Models.PickUp
{
    public class PickUpEdit
    {
        [Required]
        public int PickUpId { get; set; }
        [Required]
        public DateTime TimeOfDay { get; set; }
        [Required]
        public string PickUpName { get; set; }
        public int GuestId { get; set; }
    }
}
