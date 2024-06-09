using System.ComponentModel.DataAnnotations;

namespace AGB_Bank.ViewModels
{
    public class Register_EntrepriseVM
    {
        [Required]
        public string? typeClient { get; set; }
        [Required]
        public int? codePostal { get; set; }
        [Required]
        public float? Revenu { get; set; }

        [Required(ErrorMessage = "La date de naissance est obligatoire.")]
        [DataType(DataType.Date, ErrorMessage = "Veuillez entrer une date valide.")]
        public DateTime dateNaissance { get; set; }
        [Required]
        public string? Gender { get; set; }
        [Required]
        public string? EtatCivil { get; set; }

        [Required]
        public string? agence { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
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

        [DataType(DataType.MultilineText)]
        public string? Address { get; set; }


        
        public string? Documents { get; set; }
        
        
        public string? ActivitePrincipal { get; set; }
        public string? gl { get; set; }

        public int? Document_number { get; set; }
        public int? chiffre_affaire { get; set; }
        public int? effectif { get; set; }
        public string? nature_juridque { get; set; }
        public string? dénomination_sociale { get; set; }
    }
}
