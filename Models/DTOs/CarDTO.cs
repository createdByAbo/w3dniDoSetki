namespace w3dniDoSetki.Models.DTOs;

public class CarDTO
{
    public string Brand { get; set; }
    public int Model { get; set; }
    public string Fuel { get; set; }
    public string Gearbox { get; set; }
    public string Condition { get; set; }
    public DateTime ProdDate { get; set; }
    public int Milage { get; set; }
    public int EngCapacity { get; set; }
    public int Power { get; set; }
    public int Price { get; set; }
    public string Title { get; set; }
    public DateTime fstReg { get; set; }
    public bool RegInPl { get; set; }
    public bool NoAcc { get; set; }
    public bool FstOwr { get; set; }
    public string Description { get; set; }

    public int Vin { get; set; }
}