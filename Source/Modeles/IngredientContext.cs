using Gestion_Bunny.Modeles;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Bunny.Data
{
    public class IngredientContext : DbContext
    {
        public IngredientContext(DbContextOptions<IngredientContext> options) : base(options)
        {
        }

        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.ToTable("ingredients", "public");

                entity.Property(i => i.Id).HasColumnName("id");
                entity.Property(i => i.Name).HasColumnName("name");
                entity.Property(i => i.QuantityRemaining).HasColumnName("quantity_remaining");
                entity.Property(i => i.QuantityPerDeliveryUnit).HasColumnName("quantity_per_delivery_unit");
                entity.Property(i => i.MinimumThresholdNotification).HasColumnName("minimum_threshold_notification");
                entity.Property(i => i.IsDeleted).HasColumnName("is_deleted");
                entity.Property(i => i.DeletedDate).HasColumnName("deleted_date");

                // Vous pouvez ajouter des configurations supplémentaires si nécessaire.
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
