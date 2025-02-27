using Gestion_Bunny.Modeles;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
       
    }

    public DbSet<RestaurantProfile> RestaurantProfiles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Bill> Bills { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<RecipeCategory> RecipeCategories { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<BillRecipe> BillRecipes { get; set; }
    public DbSet<Ingredient> OrderIngredients { get; set; }
    public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<OrderIngredient>()
            .HasKey(bi => new { bi.OrderId, bi.IngredientId });

        modelBuilder.Entity<BillRecipe>()
            .HasKey(b => new { b.BillId, b.RecipeId });

        modelBuilder.Entity<RecipeIngredient>()
            .HasKey(ir => new { ir.RecipeId, ir.IngredientId });

        modelBuilder.Entity<RecipeIngredient>()
            .HasOne(ir => ir.Ingredient)
            .WithMany(i => i.RecipeIngredients) 
            .HasForeignKey(ir => ir.IngredientId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RecipeIngredient>()
            .HasOne(ir => ir.Recipe)
            .WithMany(i => i.RecipeIngredients) 
            .HasForeignKey(ir => ir.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

