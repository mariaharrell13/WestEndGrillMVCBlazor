using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System;

namespace WestEndGrillMVC.Server.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public string Entree { get; set; }
        [Required]
        public string Side { get; set; }
        [Required]
        public string Drink { get; set; }
        [Required]
        public string Desert { get; set; }
        [ForeignKey("PickUp")]
        public int PickUpId { get; set; }
    }
}
