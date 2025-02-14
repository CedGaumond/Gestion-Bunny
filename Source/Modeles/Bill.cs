using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Bunny.Modeles
{
    [Table("bills", Schema = "public")]
    public class Bill
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("date_created")]
        public DateTime OrderDate { get; set; }

        [Column("bill_file")]
        public byte[]? BillFile { get; set; } 

        [Required]
        [ForeignKey("EmployeeId")]
        [Column("employee_id")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Column("total_amount")]
        public float TotalAmount { get; set; }

        public ICollection<BillRecipe> BillRecipes { get; set; }
    }
}
