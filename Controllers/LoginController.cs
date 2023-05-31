using DevOne.Security.Cryptography.BCrypt;
using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using w3dniDoSetki.Models.DTOs;
using w3dniDoSetki.Services;

namespace w3dniDoSetki.Controllers;

public class LoginController : Controller
{
    private readonly IUserService _userService;
    
    public LoginController(IUserService userService)
    {
        _userService = userService;
    }
    
    
    // GET
    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Index(IFormCollection collection)
    {
        LoginDto LoginData = new LoginDto(collection["Email"], collection["Password"]);
        try
        {
            return RedirectToAction("Index", "Home", new { token = _userService.Login(LoginData) });
        }
        catch (Exception)
        {
            return View("Error");
        }
    }
}