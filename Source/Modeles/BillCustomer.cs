using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Bunny.Modeles
{
    [Table("bill_customer", Schema = "public")]
    public class BillCustomer
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("order_date")]
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

        public ICollection<BillItem> BillItems { get; set; }
    }
}
