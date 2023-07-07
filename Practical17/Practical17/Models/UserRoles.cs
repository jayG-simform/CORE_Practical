using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practical17.Models
{
    public class UserRoles
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        [ForeignKey("Roles")]
        public int RoleId { get; set; }
        public Users Users { get; set; }
        public Roles Roles { get; set; }
    }
}
