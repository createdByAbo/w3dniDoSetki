using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using w3dniDoSetki.Models;
using w3dniDoSetki.Models.DTOs;
using w3dniDoSetki.Services;

namespace w3dniDoSetki.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICarBrandsService _carBrandsService;
    private readonly ICarModelsService _carModelsService;
    private readonly W3dnidosetkiContext _context;

    public HomeController(ILogger<HomeController> logger,ICarModelsService carModelsService,  ICarBrandsService carBrandsService, W3dnidosetkiContext context)
    {
        _logger = logger;
        _context = context;
        _carModelsService = carModelsService;
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
        foreach (var col in collection)
        {
            Response.Cookies.Append(col.Key, col.Value.ToString());
        }
        Response.Cookies.Append("noAccidents", collection["noAcc"].Count == 0 ? "off" : "on");
        Response.Cookies.Append("firstOwn", collection["firstOwnCheckbox"].Count == 0 ? "off" : "on");
        Response.Cookies.Append("plCheckbox", collection["plCheckbox"].Count == 0 ? "off" : "on");

        return RedirectToAction("FilteredCars", "Cars");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}