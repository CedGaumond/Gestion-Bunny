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
            User = null!;
            OrderDate = DateTime.Now;
        }

        /// <summary>
        /// Génère un numéro de facture professionnel basé sur la date de la commande et l'ID de la commande.
        /// Le format du numéro de facture est : ORD-YYYY-MM-XXXX, où :
        /// - "ORD" est un préfixe indiquant qu'il s'agit d'une facture de commande.
        /// - "YYYY" représente l'année de la commande.
        /// - "MM" représente le mois de la commande.
        /// - "XXXX" est l'ID de la commande, formaté sur 4 chiffres.
        /// </summary>
        /// <returns>
        /// Un numéro de facture sous forme de chaîne de caractères au format : "ORD-YYYY-MM-XXXX".
        /// </returns>
        public string GenerateInvoiceNumber()
        {
            return $"ORD-{OrderDate:yyMM}{Id:D4}";
        }
    }
}
