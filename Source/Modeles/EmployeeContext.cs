using Gestion_Bunny.Modeles;
using Microsoft.EntityFrameworkCore;

public class EmployeeContext : DbContext
{
    public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employee { get; set; }
    public DbSet<EmployeeRole> EmployeeRole { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
                  .WithMany()  // No collection in EmployeeRole, so no navigation property needed
                  .HasForeignKey(e => e.EmployeeRoleId)
                  .OnDelete(DeleteBehavior.SetNull);  // Adjust based on your deletion policy
        });

        modelBuilder.Entity<EmployeeRole>(entity =>
        {
            // Update table name to singular
            entity.ToTable("employee_role", "public");  // Singular 'employee_role'

            entity.Property(er => er.Id).HasColumnName("id");
            entity.Property(er => er.RoleName).HasColumnName("role_name");
        });

        base.OnModelCreating(modelBuilder);
    }
}
