namespace w3dniDoSetki.Models.DTOs;

public class CarCardDto
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Title { get; set; }
    public int Price { get; set; }
    public DateOnly ProdYear { get; set; }
    public string imgPath { get; set; }
    public int carId { get; set; }
}