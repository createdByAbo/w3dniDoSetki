namespace w3dniDoSetki.Models;

public class Car
{
    public Car(int sellerId, int model, short numOfDoors, short numOfSeats, DateOnly prodYear, DateOnly? stRegistration, int milage, short engineCapacity, short enginePower, string? vin, string? condition, bool noAccidents, bool stOwner, bool registredInPl, string country, string color, float fuelRateInCity, float fuelRateInTrip, string description, string title)
    {
        SellerId = sellerId;
        Model = model;
        NumOfDoors = numOfDoors;
        NumOfSeats = numOfSeats;
        ProdYear = prodYear;
        StRegistration = stRegistration;
        Milage = milage;
        EngineCapacity = engineCapacity;
        EnginePower = enginePower;
        Vin = vin;
        this.condition = condition;
        NoAccidents = noAccidents;
        StOwner = stOwner;
        RegistredInPl = registredInPl;
        Country = country;
        Color = color;
        FuelRateInCity = fuelRateInCity;
        FuelRateInTrip = fuelRateInTrip;
        Description = description;
        Title = title;
    }

    public int SellerId { get; set; }

    public int Model { get; set; }

    public short NumOfDoors { get; set; }

    public short NumOfSeats { get; set; }

    public DateOnly ProdYear { get; set; }

    public DateOnly? StRegistration { get; set; }

    public int Milage { get; set; }

    public short EngineCapacity { get; set; }

    public short EnginePower { get; set; }

    public string? Vin { get; set; }
    public string? condition { get; set; }

    public bool NoAccidents { get; set; }

    public bool StOwner { get; set; }

    public bool RegistredInPl { get; set; }

    public string Country { get; set; } = null!;

    public string Color { get; set; } = null!;

    public float FuelRateInCity { get; set; }

    public float FuelRateInTrip { get; set; }

    public string Description { get; set; } = null!;

    public string Title { get; set; } = null!;

}