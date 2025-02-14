using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Bunny.Modeles
{
    [Table("user_role", Schema = "public")] 
    public class UserRole
    {
        [Key]
        [Column("id")] 
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Column("role_name")] 
        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
