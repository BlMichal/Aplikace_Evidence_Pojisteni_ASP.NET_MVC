using System.ComponentModel.DataAnnotations;

namespace AplikaceEvidencePojisteni.Models
{
    public class User
    {
        [Key] 
        public int UserId { get; set; }

        [StringLength(50, MinimumLength =2)]
        [Display(Name = "Jméno")]
        public string? FirstName { get; set; }

        [StringLength(50, MinimumLength =2)]
        [Display(Name = "Příjmení")]
        public string? LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Display(Name = "Telefon")]
        public int Phone { get; set; }

        [Display(Name = "Město")]
        public string? City { get; set; }

        [Display(Name = "Ulice")]
        public string? Street { get; set; }

        [Display(Name = "PSČ")]
        public int ZipCode { get; set; }

        public virtual ICollection<Insurance>? Insurances { get; set; }
    }
}
