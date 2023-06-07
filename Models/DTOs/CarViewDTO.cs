using w3dniDoSetki.Entities;

namespace w3dniDoSetki.Models.DTOs;

public class CarViewDTO
{
    public int PhoneNumber { get; set; }
    public string Title { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public int Milage { get; set; }
    public int EngineCapa { get; set; }
    public  int EnginePow { get; set; }
    public DateOnly Yeat { get; set; }
    public string path { get; set; }
    public string gearboxType { get; set; }
}