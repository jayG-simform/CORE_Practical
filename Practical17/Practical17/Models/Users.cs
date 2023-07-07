using System.ComponentModel.DataAnnotations;

namespace Practical17.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="FirstName is required")]
        [StringLength(20)]
        public string FirstName { get; set; }

        [StringLength(20)]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [Required(ErrorMessage ="Mobile number is required")]
        [Display(Name="Mobile number")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
