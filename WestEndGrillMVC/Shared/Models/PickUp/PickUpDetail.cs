using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WestEndGrillMVC.Shared.Models.PickUp
{
    public class PickUpDetail
    {
        public int PickUpId { get; set; }
        public DateTime TimeOfDay { get; set; }
        public string PickUpName { get; set; }
        public int GuestId { get; set; }
    }
}
