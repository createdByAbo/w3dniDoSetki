using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using w3dniDoSetki.Models;
using w3dniDoSetki.Services;

namespace w3dniDoSetki.Controllers;

public class CarsController : Controller
{
    private readonly ILogger<CarsController> _logger;

    public CarsController(ILogger<CarsController> logger, ICarBrandsService carBrandsService)
    {
        _logger = logger;
    }

    public IActionResult AllCars()
    {
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}