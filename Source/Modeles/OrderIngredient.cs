﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Bunny.Modeles
{
    [Table("order_ingredients", Schema = "public")]
    public class OrderIngredient
    {
        [Column("order_id")]
        public int OrderId { get; set; }
        public Order? Order { get; set; }

        [Column("ingredient_id")]
        public int IngredientId { get; set; }
        public Ingredient? Ingredient { get; set; }

        [Required]
        [Column("quantity")]
        [Range(0, double.MaxValue, ErrorMessage = "La quantité ne peut pas être négative.")]
        public int Quantity { get; set; }
    }
}
