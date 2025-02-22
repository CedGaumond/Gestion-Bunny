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
        [CustomValidation(typeof(Employee), "ValidateBirthDate")]
        public DateTime? BirthDate
        {
            get => _birthDate;
            set => _birthDate = value.HasValue
                ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc)
                : (DateTime?)null;
        }

        private DateTime? _birthDate;

        [Range(0, double.MaxValue, ErrorMessage = "Le nombre d'heures désiré doit être supérieur à zéro.")]
        [Column("number_hours_desired")]
        public decimal? NumberHoursDesired { get; set; }

        [Column("social_insurance_number")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Le NAS doit contenir exactement 9 chiffres.")]
        public string? SocialInsuranceNumber { get; set; }

        [Column("address")]
        public string? Address { get; set; }

        [Column("pic")]
        public byte[]? Pic { get; set; }

        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>(); // Relation One-to-Many

        public static ValidationResult ValidateBirthDate(DateTime? birthDate, ValidationContext context)
        {
            DateTime minDate = DateTime.Now.AddYears(-150);
            DateTime maxDate = DateTime.Now;

            if (birthDate == null)
            {
                return new ValidationResult("La date de naissance est invalide.");
            }

            if (birthDate < minDate || birthDate > maxDate)
            {
                return new ValidationResult($"La date de naissance est invalide.");
            }

            try
            {
                DateTime validDate = birthDate.Value;
                DateTime.TryParse(validDate.ToString("yyyy-MM-dd"), out validDate);

                if (validDate != birthDate.Value)
                {
                    return new ValidationResult("La date entrée n'existe pas.");
                }
            }
            catch (Exception)
            {
                return new ValidationResult("La date entrée n'est pas valide.");
            }

            return ValidationResult.Success;
        }
    }
}
