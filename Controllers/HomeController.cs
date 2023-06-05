using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using w3dniDoSetki.Models;
using w3dniDoSetki.Services;

namespace w3dniDoSetki.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICarBrandsService _carBrandsService;
    private readonly ICarModelsService _carModelsService;
    private readonly W3dnidosetkiContext _context;

    public HomeController(ILogger<HomeController> logger, ICarBrandsService carBrandsService, W3dnidosetkiContext context)
    {
        _logger = logger;
        _context = context;
        _carBrandsService = carBrandsService;
    }

    public ActionResult Index()
    {
        _carBrandsService.WriteBrandsToJson();
        return View();
    }
    
    [HttpPost]
    public ActionResult Index(IFormCollection collection)
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}