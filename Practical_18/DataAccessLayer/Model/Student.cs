using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Model
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Student name is required")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }

        public string Address { get; set; }
    }
}