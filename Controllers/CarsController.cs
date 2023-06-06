using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using w3dniDoSetki.Entities;
using w3dniDoSetki.Models;
using w3dniDoSetki.Models.DTOs;
using w3dniDoSetki.Services;
using Car = w3dniDoSetki.Models.Car;

namespace w3dniDoSetki.Controllers;

public class CarsController : Controller
{
    private readonly ILogger<CarsController> _logger;
    private readonly W3dnidosetkiContext _context;
    private readonly ICarModelsService _carModelsService;

    public CarsController(ILogger<CarsController> logger, W3dnidosetkiContext context, ICarModelsService carModelsService)
    {
        _logger = logger;
        _context = context;
        _carModelsService = carModelsService;
    }

    public IActionResult AllCars()
    {
        var data = _context.Cars1
            .ToList();
        foreach (var car in data)
        {
            Console.WriteLine(_carModelsService.GetModelAndBrandNameIdByModelName("A8")[0]);
            Console.WriteLine(_carModelsService.GetModelAndBrandNameIdByModelName("A8")[1]);
        }

        return View();
    }

    [Authorize]
    public ActionResult AddCar()
    {
        return View();
    }

    [Authorize]
    [HttpPost]
    public ActionResult AddCar(IFormCollection collection, List<IFormFile> files)
    {
        int i = 0;
        foreach (var file in files)
        {
            i++;
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine("./images", "Uploads", $"{collection["title"]}_{i}_{file.FileName}");
            Console.WriteLine(path);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }
        var handler = new JwtSecurityTokenHandler();
        int id = Int32.Parse(handler.ReadJwtToken(Request.Cookies["X-Access-Token"].Replace(" ", "")).Claims.ToList()[0].Value);

        Car1 car = new Car1();

        car.SellerId = id;
        car.Model = _carModelsService.GetModelAndBrandNameIdByModelName(collection["Model"])[1];
        car.NumOfDoors = short.Parse(collection["numOfDoors"]);
        car.NumOfSeats = short.Parse(collection["numOfSeats"]);
        car.ProdYear = DateOnly.Parse($"{collection["yearMin"]}-01-01");
        car.StRegistration = DateOnly.Parse($"{collection["1stReg"]}-01-01");
        car.Milage = Int32.Parse(collection["distMin"]);
        car.EngineCapacity = short.Parse(collection["engCapMin"]);
        car.EnginePower = short.Parse(collection["powMin"]);
        car.gearboxType = collection["gearbox"];
        car.Vin = collection["vin"];
        car.condition = collection["condition"];
        car.NoAccidents = collection["noAcc"] == "ok";
        car.StOwner = collection["firstOwnCheckbox"] == "ok";
        car.RegistredInPl = collection["plCheckbox"] == "ok";
        car.Country = (collection["plCheckbox"] == "ok" ? "Poland" : "Other");
        car.Color = "color";
        car.FuelRateInCity = float.Parse(collection["fuelRateInCity"]);
        car.FuelRateInTrip = float.Parse(collection["fuelRateInTrip"]);
        car.Description = collection["description"];
        car.Title = collection["title"];
        car.fuel = collection["fuel"];
        _context.Cars1.Add(car);
        _context.SaveChanges();



        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}