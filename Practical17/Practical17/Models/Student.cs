using System.ComponentModel.DataAnnotations;

namespace Practical17.Models
{
    public class Student
    {
        [Key]
        public int Id{ get; set; }
        [Required(ErrorMessage ="Student Name is required")]
        [StringLength(50)]
        public string StudentName { get; set; }

        public int Age { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
