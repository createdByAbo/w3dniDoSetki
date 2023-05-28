using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using w3dniDoSetki.Entities;
using w3dniDoSetki.Exceptions;


namespace w3dniDoSetki.Services;

public interface ICarsService
{
    List<Car1> GetAllCars(int limit);
}

public class CarsService : ICarsService
{
    private readonly W3dnidosetkiContext _context;

    public CarsService(W3dnidosetkiContext context)
    {
        _context = context;
    }

    public List<Car1> GetAllCars(int limit)
    {
        var cars = _context.Cars1
            .Take(limit)
            .ToList();
        return cars;
    }
}