using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WestEndGrillMVC.Shared.Models.Order
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public string Entree { get; set; }
        public string Side { get; set; }
        public string Drink { get; set; }
        public string Desert { get; set; }
        public int PickUpId { get; set; }
    }
}
