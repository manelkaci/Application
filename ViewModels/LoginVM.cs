using System.ComponentModel.DataAnnotations;

namespace AGB_Bank.ViewModels;

public class LoginVM
{

    [Required]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [Required(ErrorMessage ="Password is required.")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Display(Name = "Se souvenir de moi")]
    public bool RememberMe { get; set; }
}
