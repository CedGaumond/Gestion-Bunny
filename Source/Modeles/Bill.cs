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
        [ForeignKey("UserId")]
        [Column("user_id")]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Column("total_amount")]
        public decimal TotalAmount { get; set; }

        public ICollection<BillRecipe> BillRecipes { get; set; }

        /// <summary>
        /// Génère un numéro de facture professionnel basé sur la date de la commande et l'ID de la commande.
        /// Le format du numéro de facture est : FACT-YYYY-MM-XXXX, où :
        /// - "FACT" est un préfixe indiquant qu'il s'agit d'une facture de commande.
        /// - "YYYY" représente l'année de la commande.
        /// - "MM" représente le mois de la commande.
        /// - "XXXX" est l'ID de la commande, formaté sur 4 chiffres.
        /// </summary>
        /// <returns>
        /// Un numéro de facture sous forme de chaîne de caractères au format : "FACT-YYYY-MM-XXXX".
        /// </returns>
        public string GenerateInvoiceNumber()
        {
            return $"FACT-{OrderDate:yyMM}{Id:D4}";
        }
    }
}
