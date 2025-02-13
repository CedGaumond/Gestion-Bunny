using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Bunny.Modeles
{
    [Table("bill_provider")]
    public class BillProvider
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
        [Column("is_delivered")]
        public bool IsDelivered { get; set; }

        [Required]
        [Column("total_amount")]
        [Range(0, double.MaxValue, ErrorMessage = "Le montant total ne peut pas être négatif.")]
        public decimal TotalAmount { get; set; }

        public ICollection<BillIngredient> BillIngredients { get; set; } = new List<BillIngredient>(); // Init pour éviter null
    }
}
