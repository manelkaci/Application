using AGB_Bank.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace AGB_Bank.Controllers;

public class HomeController : Controller

{
   

    [Authorize]
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Currency()
    {
        return View();
    }

    public IActionResult account_type()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}