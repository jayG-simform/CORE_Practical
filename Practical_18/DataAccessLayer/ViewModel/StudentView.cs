using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ViewModel
{
    public class StudentView
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
