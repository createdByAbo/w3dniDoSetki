using dotenv.net;
using DotNetEnv;
using Newtonsoft.Json;

namespace w3dniDoSetki.Services;

public interface ICarBrandsService
{
    void WriteBrandsToJson();
}
public class CarBrandsService : ICarBrandsService
{
    private readonly W3dnidosetkiContext _context;

    public CarBrandsService(W3dnidosetkiContext context)
    {
        _context = context;
    }

    public void WriteBrandsToJson()
    {
        var json = _context.Cars
            .ToList();
        List<string> brands = new List<string>();
        for (int i = 0; i < json.Count; i++)
        {
            brands.Add(json[i].Make);
        }
        File.WriteAllText("./JSON/brands.json", JsonConvert.SerializeObject(brands));
    }
}