using System.ComponentModel.DataAnnotations;

namespace AplikaceEvidencePojisteni.Models
{
    public class User
    {
        [Key] 
        public int UserId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Jméno musí mít minimálně 2 a maximálně 50 znaků.")]
        [Display(Name = "Jméno")]
        public string FirstName { get; set; } = "";

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Příjmení musí mít minimálně 2 a maximálně 50 znaků.")]
        [Display(Name = "Příjmení")]
        public string LastName { get; set; } = "";

        [Required]
        [EmailAddress(ErrorMessage = "Neplatná emailová adresa")]
        public string Email { get; set; } = "";

        [Display(Name = "Telefon")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Telefonní číslo musí mít 9 čísel.")]
        public int Phone { get; set; }

        [Required]
        [Display(Name = "Město")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Město musí mít minimálně 2 a maximálně 50 znaků.")]
        public string City { get; set; } = "";

        [Required]
        [Display(Name = "Ulice")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Ulice musí mít minimálně 1 a maximálně 50 znaků.")]
        public string Street { get; set; } = "";

        [Display(Name = "PSČ")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "PSČ musí mít 5 čísel.")]
        public int ZipCode { get; set; }

        public virtual ICollection<Insurance>? Insurances { get; set; }
    }
}
