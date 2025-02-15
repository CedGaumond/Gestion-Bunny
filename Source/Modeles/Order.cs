using System.ComponentModel;
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
        [ForeignKey("UserId")]
        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        [Column("total_amount")]
        [Range(0, double.MaxValue, ErrorMessage = "Le montant total ne peut pas être négatif.")]
        public decimal TotalAmount { get; set; }

        public ICollection<OrderIngredient> OrderIngredients { get; set; } = new List<OrderIngredient>();

        public Order()
        {
            OrderDate = DateTime.Now;
        }
    }
}
