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
            Response.Cookies.Append("X-Access-Token", Token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            return RedirectToAction("Index", "Home");
        }
        catch (Exception)
        {
            return View("Error");
        }
    }

    [HttpGet]
    public ActionResult Logout()
    {
        Response.Cookies.Delete("X-Access-Token");
        return RedirectToAction("Index", "Home");
    }
}