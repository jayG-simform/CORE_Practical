using System.ComponentModel.DataAnnotations;

namespace Practical17.Models
{
    public class Roles
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string UserRole { get; set; }
    }
}
