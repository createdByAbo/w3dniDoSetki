using Microsoft.AspNetCore.Mvc;

namespace w3dniDoSetki.Controllers;

public class LoginController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}