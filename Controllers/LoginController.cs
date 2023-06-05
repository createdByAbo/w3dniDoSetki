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
            var Token = _userService.Login(LoginData);
            Response.Headers.Add("Set-Cookie", Token);
            return RedirectToAction("Index", "Home", new { token = Token });
        }
        catch (Exception)
        {
            return View("Error");
        }
    }
}