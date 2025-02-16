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

        [Column("birth_date")]
        public DateTime? BirthDate { get; set; }

        [Column("social_insurance_number")]
        public string SocialInsuranceNumber { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("pic")]
        public byte[] Pic { get; set; }

        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>(); // Relation One-to-Many
    }
}
