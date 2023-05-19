using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplikaceEvidencePojisteni.Models
{
    public class Insurance
    {
        [Key]
        public int InsuranceId { get; set; }

        [StringLength(50, MinimumLength =2)]
        [Display(Name = "Pojištění")]
        public string? InsuranceType { get; set; }

        [StringLength(50, MinimumLength =2)]
        [Display(Name = "Předmět pojištění")]
        public string? Subject { get; set; }
        
        [Display(Name = "Částka")]
        public decimal Amount { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Platnost od")]
        public DateTime ValidFrom { get; set; }

        [Display(Name = "Platnost do")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ValidUntil { get; set; }
        public int UserId { get; set; }
        
        public User? User { get; set; }
    }
}
