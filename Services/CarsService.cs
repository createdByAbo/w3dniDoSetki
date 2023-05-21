using w3dniDoSetki.Entities;
using w3dniDoSetki.Exceptions;


namespace w3dniDoSetki.Services;

public interface ICarsController
{
    List<Car1> GetCarsByUserId(int id);
}

public class CarsController : ICarsController
{
    private readonly W3dnidosetkiContext _context;

    public CarsController(W3dnidosetkiContext context)
    {
        _context = context;
    }
    
    public List<Car1> GetCarsByUserId(int id)
    {
        try
        {
            var cars = _context.Cars1
                .Where(c1 => c1.SellerId == id)
                .ToList();
            return cars;
        }
        catch (Exception)
        {
            throw new NotFoundException();
        }
    }
}