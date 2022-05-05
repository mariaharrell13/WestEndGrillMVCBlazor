using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WestEndGrillMVC.Shared.Models.Order
{
    internal class OrderEdit
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public string Entree { get; set; }
        [Required]
        public string Side { get; set; }
        [Required]
        public string Drink { get; set; }
        [Required]
        public string Desert { get; set; }
        public int PickUpId { get; set; }
    }
}
