using Gestion_Bunny.Modeles;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
      {
      }

      public DbSet<RestaurantProfile> RestaurantProfiles { get; set; }
      public DbSet<EmployeeRole> EmployeeRoles { get; set; }
      public DbSet<Employee> Employees { get; set; }
      public DbSet<Schedule> Schedules { get; set; }
      public DbSet<BillCustomer> BillCustomers { get; set; }
      public DbSet<BillProvider> BillProviders { get; set; }
      public DbSet<ItemCategory> ItemCategories { get; set; }
      public DbSet<Item> Items { get; set; }
      public DbSet<Ingredient> Ingredients { get; set; }
      public DbSet<BillItem> BillItems { get; set; }
      public DbSet<BillIngredient> BillIngredients { get; set; }
      public DbSet<ItemRecipe> ItemRecipes { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RestaurantProfile>(entity =>
            {
                  entity.ToTable("restaurant_profile", "public");

                  // Map columns
                  entity.Property(e => e.Id).HasColumnName("id");
                  entity.Property(e => e.RestaurantName).HasColumnName("restaurant_name");
                  entity.Property(e => e.RestaurantAddress).HasColumnName("restaurant_address");
                  entity.Property(e => e.OpeningHours).HasColumnName("opening_hours");
            });


            modelBuilder.Entity<EmployeeRole>(entity =>
            {
                  // Update table name to singular
                  entity.ToTable("employee_role", "public");  // Singular 'employee_role'

                  // Map columns
                  entity.Property(e => e.Id).HasColumnName("id");
                  entity.Property(e => e.RoleName).HasColumnName("role_name");

                  // Configure the relationship with Employee
                  entity.HasMany(e => e.Employees)  // One-to-many relationship with Employee
                    .WithOne(e => e.EmployeeRole)  // Each Employee has one EmployeeRole
                    .HasForeignKey(e => e.EmployeeRoleId)  // Foreign key in Employee
                    .OnDelete(DeleteBehavior.SetNull);  // Adjust based on your deletion policy
            });


            // Configuration for Employee
            modelBuilder.Entity<Employee>(entity =>
            {
                  // Update table name to singular
                  entity.ToTable("employees", "public");  // Singular 'employee'

                  entity.Property(e => e.Id).HasColumnName("id");
                  entity.Property(e => e.FirstName).HasColumnName("first_name");
                  entity.Property(e => e.LastName).HasColumnName("last_name");
                  entity.Property(e => e.Email).HasColumnName("e_mail");
                  entity.Property(e => e.BirthDate).HasColumnName("birth_date");
                  entity.Property(e => e.SocialInsuranceNumber).HasColumnName("social_insurance_number");
                  entity.Property(e => e.EmployeeRoleId).HasColumnName("employee_role_id");
                  entity.Property(e => e.Pic).HasColumnName("pic");
                  entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
                  entity.Property(e => e.PasswordSalt).HasColumnName("password_salt");
                  entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                  entity.Property(e => e.DeletedDate).HasColumnName("deleted_date");
                  entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
                  entity.Property(e => e.TempPassword).HasColumnName("temp_password");

                  // Configure the relationship with EmployeeRole
                  entity.HasOne(e => e.EmployeeRole)
                    .WithMany(er => er.Employees)
                    .HasForeignKey(e => e.EmployeeRoleId)
                    .OnDelete(DeleteBehavior.SetNull);  // Adjust based on your deletion policy
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                  // Define table name
                  entity.ToTable("schedule", "public");

                  // Map columns
                  entity.Property(e => e.Id).HasColumnName("id");
                  entity.Property(e => e.ShiftStart).HasColumnName("shift_start");
                  entity.Property(e => e.ShiftEnd).HasColumnName("shift_end");
                  entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                  // Configure the relationship with Employee
                  entity.HasOne(e => e.Employee)
                    .WithMany(e => e.Schedules)  // One-to-many relationship with Employee
                    .HasForeignKey(e => e.EmployeeId)  // Foreign key in Schedule
                    .OnDelete(DeleteBehavior.Restrict);  // Prevent delete action for related Employee (if needed)
            });


            modelBuilder.Entity<BillCustomer>(entity =>
            {
                  // Define table name
                  entity.ToTable("bill_customer", "public");

                  // Map columns
                  entity.Property(bc => bc.Id).HasColumnName("id");
                  entity.Property(bc => bc.OrderDate).HasColumnName("order_date");
                  entity.Property(bc => bc.BillFile).HasColumnName("bill_file");
                  entity.Property(bc => bc.EmployeeId).HasColumnName("employee_id");

                  // Configure the relationship with Employee
                  entity.HasOne(bc => bc.Employee)
                    .WithMany(e => e.BillCustomers)  // One-to-many relationship with Employee
                    .HasForeignKey(bc => bc.EmployeeId)  // Foreign key in BillCustomer
                    .OnDelete(DeleteBehavior.Restrict);  // Adjust delete behavior as needed

                  // Configure the relationship with BillItem (if applicable)
                  entity.HasMany(bc => bc.BillItems)
                    .WithOne()  // Add back-reference if applicable
                    .HasForeignKey(bi => bi.BillCustomerId)  // Assuming BillItem has BillCustomerId
                    .OnDelete(DeleteBehavior.Cascade);  // Adjust delete behavior

                  // Configure the relationship with BillIngredient (if applicable)
                  entity.HasMany(bc => bc.BillIngredients)
                    .WithOne()  // Add back-reference if applicable
                    .HasForeignKey(bi => bi.BillCustomerId)  // Assuming BillIngredient has BillCustomerId
                    .OnDelete(DeleteBehavior.Cascade);  // Adjust delete behavior
            });

            modelBuilder.Entity<ItemCategory>(entity =>
            {
                  // Define table name and schema
                  entity.ToTable("item_category", "public");

                  // Map columns
                  entity.Property(ic => ic.Id).HasColumnName("id");
                  entity.Property(ic => ic.Name).HasColumnName("name");

                  // Configure the one-to-many relationship with Item
                  entity.HasMany(ic => ic.Items)  // ItemCategory can have many Items
                    .WithOne()  // Assuming Item has a reference to ItemCategory (not required if Item doesn't have navigation back)
                    .HasForeignKey(i => i.ItemCategoryId)  // Foreign key in Item, assuming Item has ItemCategoryId
                    .OnDelete(DeleteBehavior.Cascade);  // Adjust delete behavior as necessary
            });


            modelBuilder.Entity<Item>(entity =>
            {
                  entity.ToTable("items", "public");

                  entity.Property(i => i.Id).HasColumnName("id");
                  entity.Property(i => i.Name).HasColumnName("name").HasMaxLength(255).IsRequired();
                  entity.Property(i => i.Price).HasColumnName("price").IsRequired();
                  entity.Property(i => i.Pic).HasColumnName("pic");
                  entity.Property(i => i.DeletedDate).HasColumnName("deleted_date");
                  entity.Property(i => i.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false);

                  // Foreign key relationship with ItemCategory
                  entity.HasOne(i => i.ItemCategory)
                    .WithMany(ic => ic.Items)
                    .HasForeignKey(i => i.ItemCategoryId)
                    .OnDelete(DeleteBehavior.Restrict); // �viter la suppression en cascade
            });


            modelBuilder.Entity<Ingredient>(entity =>
            {
                  entity.ToTable("ingredients", "public");

                  entity.Property(i => i.Id).HasColumnName("id");

                  entity.Property(i => i.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsRequired();

                  entity.Property(i => i.QuantityRemaining)
                    .HasColumnName("quantity_remaining")
                    .IsRequired();

                  entity.Property(i => i.QuantityPerDeliveryUnit)
                    .HasColumnName("quantity_per_delivery_unit")
                    .IsRequired();

                  entity.Property(i => i.MinimumThresholdNotification)
                    .HasColumnName("minimum_threshold_notification")
                    .IsRequired();

                  entity.Property(i => i.DeletedDate)
                    .HasColumnName("deleted_date");

                  entity.Property(i => i.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasDefaultValue(false);
            });


            modelBuilder.Entity<BillItem>(entity =>
            {
                  entity.ToTable("bill_items", "public");

                  entity.HasKey(bi => new { bi.BillCustomerId, bi.ItemId });

                  entity.Property(bi => bi.BillCustomerId)
                    .HasColumnName("bill_customer_id")
                    .IsRequired();

                  entity.Property(bi => bi.ItemId)
                    .HasColumnName("item_id")
                    .IsRequired();

                  entity.Property(bi => bi.Quantity)
                    .HasColumnName("quantity")
                    .IsRequired()
                    .HasColumnType("NUMERIC")
                    .HasDefaultValue(0);

                  entity.Property(bi => bi.QuantityDeleted)
                    .HasColumnName("quantity_deleted")
                    .IsRequired()
                    .HasDefaultValue(0);

                  entity.HasOne(bi => bi.BillCustomer)
                    .WithMany(bc => bc.BillItems)
                    .HasForeignKey(bi => bi.BillCustomerId)
                    .OnDelete(DeleteBehavior.NoAction);

                  entity.HasOne(bi => bi.Item)
                    .WithMany(i => i.BillItems)
                    .HasForeignKey(bi => bi.ItemId)
                    .OnDelete(DeleteBehavior.NoAction);
            });


            modelBuilder.Entity<BillIngredient>(entity =>
            {
                  entity.ToTable("bill_ingredients", "public");

                  entity.HasKey(bi => new { bi.BillCustomerId, bi.IngredientId });

                  entity.Property(bi => bi.BillCustomerId)
                    .HasColumnName("bill_customer_id")
                    .IsRequired();

                  entity.Property(bi => bi.IngredientId)
                    .HasColumnName("ingredient_id")
                    .IsRequired();

                  entity.Property(bi => bi.Quantity)
                    .HasColumnName("quantity")
                    .IsRequired()
                    .HasColumnType("NUMERIC")
                    .HasDefaultValue(0);

                  entity.HasOne(bi => bi.BillCustomer)
                    .WithMany(bc => bc.BillIngredients)
                    .HasForeignKey(bi => bi.BillCustomerId)
                    .OnDelete(DeleteBehavior.Cascade);

                  entity.HasOne(bi => bi.Ingredient)
                    .WithMany(i => i.BillIngredients)
                    .HasForeignKey(bi => bi.IngredientId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ItemRecipe>(entity =>
            {
                  entity.ToTable("item_recipe", "public");

                  entity.HasKey(ir => new { ir.ItemId, ir.IngredientId });

                  entity.Property(ir => ir.ItemId)
                    .HasColumnName("item_id")
                    .IsRequired();

                  entity.Property(ir => ir.IngredientId)
                    .HasColumnName("ingredient_id")
                    .IsRequired();

                  entity.Property(ir => ir.Quantity)
                    .HasColumnName("quantity")
                    .IsRequired()
                    .HasColumnType("NUMERIC")
                    .HasDefaultValue(0);

                  entity.HasOne(ir => ir.Item)
                    .WithMany(i => i.ItemRecipes)
                    .HasForeignKey(ir => ir.ItemId)
                    .OnDelete(DeleteBehavior.Cascade);

                  entity.HasOne(ir => ir.Ingredient)
                    .WithMany(i => i.ItemRecipes)
                    .HasForeignKey(ir => ir.IngredientId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

      }
}

