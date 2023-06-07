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
    private readonly ICarBrandsService _carBrandsService;
    private readonly ICarsService _carsService;

    public CarsController(ILogger<CarsController> logger, W3dnidosetkiContext context, ICarBrandsService carBrandsService,  ICarModelsService carModelsService, ICarsService carsService)
    {
        _logger = logger;
        _context = context;
        _carModelsService = carModelsService;
        _carBrandsService = carBrandsService;
        _carsService = carsService;
    }

    public ActionResult Index(int id = -1)
    {
        if (id > 0)
        {
            var car = _context.Cars1
                .Where(c => c.Id == id)
                .FirstOrDefault();
            CarViewDTO carData = new CarViewDTO();
            carData.PhoneNumber = Int32.Parse(_context.Users
                .Where(u => u.Id == car.SellerId)
                .FirstOrDefault().PhoneNumber);
            carData.path = car.path;
            carData.Description = car.Description;
            carData.gearboxType = car.gearboxType;
            carData.Milage = car.Milage;
            carData.Price = car.price;
            carData.Title = car.Title;
            carData.Yeat = car.ProdYear;
            carData.EngineCapa = car.EngineCapacity;
            carData.EnginePow = car.EnginePower;
            return View(carData);
        }
        else
        {
            return RedirectToAction("AllCars");
        }
    }
    public IActionResult AllCars(int page = 0, int deltaP= 10, int maxp = 10)
    {
        var data = _carsService.GetAllCars(page * deltaP, deltaP);
        List<CarCardDto> res = new List<CarCardDto>();
        for (int i = 0; i < data.Count; i++)
        {
            CarCardDto tempData = new CarCardDto();
            tempData.ProdYear = data[i].ProdYear;
            tempData.Title = data[i].Title;
            tempData.Price = data[i].price;
            tempData.Brand = _carBrandsService.GetBrandNameById(_carModelsService.GetBrandIdByModelId(data[i].Model));
            tempData.Model = _carModelsService.GetModelNameById(data[i].Model);
            tempData.imgPath = data[i].path;
            tempData.carId = data[i].Id;
            res.Add(tempData);
        }
        return View(res);
    }
    
    public IActionResult FilteredCars()
    {
        var data = _context.Cars1
            .Where(c => c.gearboxType ==
                        (Request.Cookies["gearbox"] == "Wszystkie" ? c.gearboxType : Request.Cookies["gearbox"]))
            .Where(c => c.condition ==
                        (Request.Cookies["condition"] == "Wszystkie" ? c.condition : Request.Cookies["condition"]))
            .Where(c => c.fuel == (Request.Cookies["fuel"] == "Wszystkie" ? c.fuel : Request.Cookies["fuel"]))
            .Where(c => c.ProdYear >= (Request.Cookies["yearMin"].Length == 0
                ? c.ProdYear
                : DateOnly.Parse($"{Request.Cookies["yearMin"]}-01-01")))
            .Where(c => c.ProdYear <= (Request.Cookies["yearMax"].Length == 0
                ? c.ProdYear
                : DateOnly.Parse($"{Request.Cookies["yearMax"]}-12-31")))
            .Where(c => c.Milage >= (Request.Cookies["distMin"].Length == 0
                ? c.Milage
                : Int32.Parse(Request.Cookies["distMin"])))
            .Where(c => c.Milage <= (Request.Cookies["distMax"].Length == 0
                ? c.Milage
                : Int32.Parse(Request.Cookies["distMax"])))
            .Where(c => c.EngineCapacity >= (Request.Cookies["engCapMin"].Length == 0
                ? c.EngineCapacity
                : Int32.Parse(Request.Cookies["engCapMin"])))
            .Where(c => c.EngineCapacity <= (Request.Cookies["engCapMax"].Length == 0
                ? c.EngineCapacity
                : Int32.Parse(Request.Cookies["engCapMax"])))
            .Where(c => c.EnginePower >= (Request.Cookies["powMin"].Length == 0
                ? c.EnginePower
                : Int32.Parse(Request.Cookies["powMin"])))
            .Where(c => c.EnginePower <= (Request.Cookies["powMax"].Length == 0
                ? c.EnginePower
                : Int32.Parse(Request.Cookies["powMax"])))
            .Where(c => c.price >= (Request.Cookies["priceMin"].Length == 0
                ? c.price
                : Int32.Parse(Request.Cookies["priceMin"])))
            .Where(c => c.price <= (Request.Cookies["priceMax"].Length == 0
                ? c.price
                : Int32.Parse(Request.Cookies["priceMax"])))
            .Where(c => c.NoAccidents == (Request.Cookies["noAccidents"] == "on"))
            .Where(c => c.StOwner == (Request.Cookies["firstOwn"] == "on"))
            .Where(c => c.RegistredInPl == (Request.Cookies["plCheckbox"] == "on"))
            .Where(c => c.Model == (Request.Cookies["model"].Length == 0
                ? c.Model
                : _carModelsService.GetModelAndBrandNameIdByModelName(Request.Cookies["model"])[1]))
            .ToList();
        List<CarCardDto> res = new List<CarCardDto>();
        for (int i = 0; i < data.Count; i++)
        {
            CarCardDto tempData = new CarCardDto();
            tempData.ProdYear = data[i].ProdYear;
            tempData.Title = data[i].Title;
            tempData.Price = data[i].price;
            tempData.Brand = _carBrandsService.GetBrandNameById(_carModelsService.GetBrandIdByModelId(data[i].Model));
            tempData.Model = _carModelsService.GetModelNameById(data[i].Model);
            tempData.imgPath = data[i].path;
            tempData.carId = data[i].Id;
            res.Add(tempData);
        }
        return View(res);
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
        string pathGlob = "";
        foreach (var file in files)
        {
            i++;
            var fileName = Path.GetExtension(file.FileName);
            var path = Path.Combine("wwwroot/images/Uploads", $"{collection["title"]}_{i}{fileName}");
            pathGlob = path;
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
        Console.WriteLine(collection["Model"]);
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
        car.price = Int32.Parse(collection["price"]);
        car.fuel = collection["fuel"];
        car.path = pathGlob;
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