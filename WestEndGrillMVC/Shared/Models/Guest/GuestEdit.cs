using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WestEndGrillMVC.Shared.Models.Guest
{
    public class GuestEdit
    {
        [Required]
        public int GuestId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
    }
}
