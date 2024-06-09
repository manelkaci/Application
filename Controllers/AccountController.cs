using AGB_Bank.Models;
using AGB_Bank.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace AGB_Bank.Controllers;
//IHttpContextAccessor httpContextAccessor
public class AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager) : Controller
{
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (ModelState.IsValid)
        {
            //login
            var result = await signInManager.PasswordSignInAsync(model.Email!, model.Password!, model.RememberMe, false);

            if (result.Succeeded)
            {
                //return RedirectToLocal(returnUrl);
                return RedirectToAction("Run", "Python");
            }

            ModelState.AddModelError("", "Invalid login attempt");
        }
        return View(model);
    }
    [Authorize]
    public async Task<IActionResult> UserDetails()
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }
    public IActionResult Register_Entreprise(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    public IActionResult Register_Professionel(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    public IActionResult Register_Particulier(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }
    public string GenerateRandomString(int length = 8)
    {
        const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        var randomString = new StringBuilder(length);

        for (int i = 0; i < length; i++)
        {
            randomString.Append(Chars[random.Next(Chars.Length)]);
        }

        return randomString.ToString();
    }

    [HttpPost]
    public async Task<IActionResult> Register_Particulier(Register_ParticulierVM model, string? returnUrl = "Home/Index")
    {

        returnUrl ??= Url.Content("~/");

        if (ModelState.IsValid)
        {
            AppUser user = new()
            {
                typeClient = model.typeClient,
                mot_passe = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName,
                gl = model.gl,
                agence = model.agence,
                dateNaissance = model.dateNaissance,
                PhoneNumber = model.PhoneNumber,
                PaysNaissance = model.PaysNaissance,
                EtatCivil = model.EtatCivil,
                Gender = model.Gender,
                Objet = model.Objet,
                Profession = model.Profession,
                Revenu = model.Revenu,
                codePostal = model.codePostal,
                Address = model.Address,
                Email = model.Email,
                UserName = model.Email,
            };

            var result = await userManager.CreateAsync(user, model.Password!);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);

                // Appeler la fonction JavaScript pour afficher le popup
                ViewData["ShowSuccessPopup"] = true;
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Register_Professionel(Register_ProfessionelVM model, string? returnUrl = "Home/Index")
    {
        returnUrl ??= Url.Content("~/");

        if (ModelState.IsValid)
        {
            AppUser user = new()
            {
                typeClient = model.typeClient,
                FirstName = model.FirstName,
                LastName = model.LastName,
                mot_passe = model.Password,
                gl = model.gl,
                dateNaissance = model.dateNaissance,
                agence = model.agence,
                PhoneNumber = model.PhoneNumber,
                PaysNaissance = model.PaysNaissance,
                EtatCivil = model.EtatCivil,
                Gender = model.Gender,
                //Objet = model.Objet,
                Profession = model.Profession,
                Revenu = model.Revenu,
                codePostal = model.codePostal,
                Address = model.Address,
                Documents = model.Documents,
                Registre = model.Registre,
                ActivitePrincipal = model.ActivitePrincipal,
                Email = model.Email,
                UserName = model.Email,
            };

            var result = await userManager.CreateAsync(user, model.Password!);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                ViewData["ShowSuccessPopup"] = true;

                //return RedirectToLocal(urlAvecReturnUrl);
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Register_Entreprise(Register_EntrepriseVM model, string? returnUrl = "Home/Index")
    {
        returnUrl ??= Url.Content("~/");

        if (ModelState.IsValid)
        {
            AppUser user = new()
            {
                typeClient = model.typeClient,
                dateNaissance = model.dateNaissance,
                agence = model.agence,
                PhoneNumber = model.PhoneNumber,
                gl = model.gl,
                mot_passe = model.Password,
                Revenu = model.Revenu,
                codePostal = model.codePostal,
                Address = model.Address,
                Documents = model.Documents,
                ActivitePrincipal = model.ActivitePrincipal,
                Email = model.Email,
                UserName = model.Email,
                Document_number = model.Document_number,
                chiffre_affaire = model.chiffre_affaire,
                effectif = model.effectif,
                nature_juridque = model.nature_juridque,
                dénomination_sociale = model.dénomination_sociale
            };

            var result = await userManager.CreateAsync(user, model.Password!);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                ViewData["ShowSuccessPopup"] = true;

            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        //return RedirectToAction("Logout","Python");
        return RedirectToAction("Login", "Account");
    }

    private IActionResult RedirectToLocal(string? returnUrl)
    {
        return !string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)
            ? Redirect(returnUrl)
            : RedirectToAction(nameof(HomeController.Index), nameof(HomeController));
    }
}
