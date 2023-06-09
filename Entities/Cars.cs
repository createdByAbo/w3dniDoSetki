﻿using System;
using System.Collections.Generic;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace w3dniDoSetki.Entities;

public partial class Car1
{
    public int Id { get; set; }

    public string path { get; set; }
    public int SellerId { get; set; }

    public int Model { get; set; }

    public short NumOfDoors { get; set; }
    public string fuel { get; set; }
    
    public string gearboxType { get; set; }
    public int price { get; set; }

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

    public virtual User Seller { get; set; } = null!;
}
