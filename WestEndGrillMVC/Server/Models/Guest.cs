using System.ComponentModel.DataAnnotations;

namespace WestEndGrillMVC.Server.Models
{
    public class Guest
    {
        [Key]
        public int GuestId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
    }
}
 