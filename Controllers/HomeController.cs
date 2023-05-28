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

    public HomeController(ILogger<HomeController> logger, ICarBrandsService carBrandsService)
    {
        _logger = logger;
        _carBrandsService = carBrandsService;
    }

    public IActionResult Index()
    {
        _carBrandsService.WriteBrandsToJson();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}