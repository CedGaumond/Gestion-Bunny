using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Bunny.Modeles
{
    [Table("orders")]
    public class Order
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("date_created")]
        public DateTime OrderDate { get; set; }

        [Column("order_file")]
        public byte[]? OrderFill { get; set; }

        [Required]
        [Column("is_delivered")]
        public bool IsDelivered { get; set; }

        [Required]
        [ForeignKey("EmployeeId")]
        [Column("employee_id")]
        public int EmployeeId { get; set; }

        [Required]
        [Column("total_amount")]
        [Range(0, double.MaxValue, ErrorMessage = "Le montant total ne peut pas être négatif.")]
        public decimal TotalAmount { get; set; }

        public ICollection<Ingredient> OrderIngredients { get; set; } = new List<Ingredient>(); 
    }
}
