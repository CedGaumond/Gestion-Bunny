using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Bunny.Modeles
{
    [Table("bill_items", Schema = "public")]  
    public class BillItem
    {
        [Column("bill_customer_id")] 
        public int BillCustomerId { get; set; }
        public BillCustomer BillCustomer { get; set; }

        [Column("item_id")]  
        public int ItemId { get; set; }
        public Item Item { get; set; }

        [Required]
        [Column("quantity")]  
        [Range(0, double.MaxValue, ErrorMessage = "La quantité ne peut pas être négative.")]
        public decimal Quantity { get; set; }

        [Required]
        [Column("quantity_deleted")] 
        public int QuantityDeleted { get; set; }
    }
}
