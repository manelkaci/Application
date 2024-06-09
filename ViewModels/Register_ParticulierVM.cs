using System.ComponentModel.DataAnnotations;

namespace AGB_Bank.ViewModels
{
    public class Register_ParticulierVM
    {
        
        [Required]
        public string? typeClient { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "La date de naissance est obligatoire.")]
        [DataType(DataType.Date, ErrorMessage = "Veuillez entrer une date valide.")]
        public DateTime dateNaissance { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        public string? gl { get; set; }
        [Required]
        public string? agence { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords don't match.")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string? Address { get; set; }

        [Required]
        public int? codePostal { get; set; }
        [Required]
        public float? Revenu { get; set; }

        public string? Profession { get; set; }
        
        public string? Objet { get; set; }
        [Required]
        public string? Gender { get; set; }
        [Required]
        public string? EtatCivil { get; set; }
        [Required]
        public string? PaysNaissance { get; set; }

    }

}
