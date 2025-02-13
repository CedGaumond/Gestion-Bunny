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

        modelBuilder.Entity<BillIngredient>()
        .HasKey(bi => new { bi.BillCustomerId, bi.IngredientId });

        modelBuilder.Entity<BillItem>()
       .HasKey(b => new { b.BillCustomerId, b.ItemId });

        modelBuilder.Entity<ItemRecipe>()
            .HasKey(ir => new { ir.ItemId, ir.IngredientId });

        modelBuilder.Entity<ItemRecipe>()
            .HasOne(ir => ir.Ingredient)
            .WithMany(i => i.ItemRecipes) 
            .HasForeignKey(ir => ir.IngredientId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ItemRecipe>()
            .HasOne(ir => ir.Item)
            .WithMany(i => i.ItemRecipes) 
            .HasForeignKey(ir => ir.ItemId)
            .OnDelete(DeleteBehavior.Cascade);


    }
}

