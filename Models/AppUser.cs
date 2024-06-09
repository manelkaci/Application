using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AGB_Bank.Models;

public class AppUser : IdentityUser
{
    public string? mot_passe { get; set; }
    [Required]
    public string? typeClient { get; set; }
    
    public int? codePostal { get; set; }
    public string? gl { get; set; }

    [Required]
    public string? agence { get; set; }
    public float? Revenu { get; set; }

    [DataType(DataType.Date, ErrorMessage = "Veuillez entrer une date valide.")]
    public DateTime? dateNaissance { get; set; }

    [StringLength(100)]
    [MaxLength(100)]
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public string? Profession { get; set; }
    public string? Objet { get; set; }
    
    public string? Gender { get; set; }
    
    public string? EtatCivil { get; set; }
    
    public string? PaysNaissance { get; set; }
    
    public string? Address { get; set; }

    // professionel
    public string? Documents { get; set; }
    public string? Registre { get; set; }
    public string? ActivitePrincipal { get; set; }

    // entreprise

    public int? Document_number { get; set; }
    public int? chiffre_affaire { get; set; }
    public int? effectif { get; set; }
    public string? nature_juridque { get; set; }
    public string? dénomination_sociale { get; set; }

}
