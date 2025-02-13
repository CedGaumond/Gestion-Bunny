using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Bunny.Modeles
{
    [Table("employees", Schema = "public")]
    public class Employee
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("e_mail")]
        public string Email { get; set; }

        [Column("birth_date")]
        public DateTime? BirthDate { get; set; }

        [Column("social_insurance_number")]
        public string SocialInsuranceNumber { get; set; }

        [Column("employee_role_id")]
        public int? EmployeeRoleId { get; set; }

        [Column("pic")]
        public byte[] Pic { get; set; }

        [Column("password_hash")]
        public string PasswordHash { get; set; }

        [Column("password_salt")]
        public string PasswordSalt { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("deleted_date")]
        public DateTime? DeletedDate { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        [Column("temp_password")]
        public Boolean TempPassword { get; set; }

        // Relation avec EmployeeRole
        [ForeignKey("EmployeeRoleId")]
        public EmployeeRole EmployeeRole { get; set; }

        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>(); // Relation One-to-Many
    }
}
