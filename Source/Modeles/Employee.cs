using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Bunny.Modeles
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        [StringLength(9)]
        public string SocialInsuranceNumber { get; set; }

        public int? EmployeeRoleId { get; set; }

        [ForeignKey("EmployeeRoleId")]
        public EmployeeRole EmployeeRole { get; set; }

        public byte[]? Pic { get; set; }

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(255)]
        public string PasswordSalt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? DeletedDate { get; set; }

        public bool IsDeleted { get; set; }

        public bool TempPassword { get; set; }

        //public decimal NumberHoursDesired { get; set; }

        public List<Schedule> Schedules { get; set; } = new List<Schedule>();
        public List<BillCustomer> BillCustomers { get; set; } = new List<BillCustomer>();
    }
}
