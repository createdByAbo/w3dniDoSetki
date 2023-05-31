using Microsoft.AspNetCore.Mvc;

namespace w3dniDoSetki.Views;

public class RegisterController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}