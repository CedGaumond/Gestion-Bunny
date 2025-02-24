using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Gestion_Bunny.Modeles
{
    [Table("users", Schema = "public")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("e_mail")]
        public string? Email { get; set; }

        [Column("first_name")]
        public string? FirstName { get; set; }

        [Column("last_name")]
        public string? LastName { get; set; }

        [Column("user_role_id")]
        public int? UserRoleId { get; set; }

        [ForeignKey("UserRoleId")]
        public UserRole? UserRole { get; set; }

        [Column("password_hash")]
        public string? PasswordHash { get; set; }

        [Column("password_salt")]
        public string? PasswordSalt { get; set; }

        [Column("created_date")]
        public DateTime CreatedAt { get; set; }

        [Column("deleted_date")]
        public DateTime? DeletedDate { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        [Column("temp_password")]
        public Boolean TempPassword { get; set; }

        [Column("employee_id")]
        public int? EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }

        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

        public string GetInitials()
        {
            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
                return string.Empty;

            return $"{FirstName[0]}{LastName[0]}".ToUpper();
        }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}