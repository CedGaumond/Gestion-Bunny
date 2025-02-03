using System.ComponentModel.DataAnnotations;

namespace Gestion_Bunny.Modeles
{
    public class EmployeeRole
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string RoleName { get; set; }
    }
}
