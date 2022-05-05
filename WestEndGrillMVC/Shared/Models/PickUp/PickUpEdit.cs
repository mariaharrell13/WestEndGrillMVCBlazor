using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WestEndGrillMVC.Shared.Models.PickUp
{
    internal class PickUpEdit
    {
        [Required]
        public int PickUpId { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public string PickUpName { get; set; }
        public int GuestId { get; set; }
    }
}
