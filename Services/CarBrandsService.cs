using System.Security.Cryptography;
using dotenv.net;
using DotNetEnv;
using Newtonsoft.Json;

namespace w3dniDoSetki.Services;

public interface ICarBrandsService
{
    void WriteBrandsToJson();
    string GetBrandNameById(int id);
    int GetBrandIdByModelId(int modelId);
}
public class CarBrandsService : ICarBrandsService
{
    private readonly W3dnidosetkiContext _context;

    public CarBrandsService(W3dnidosetkiContext context)
    {
        _context = context;
    }

    public int GetBrandIdByModelId(int modelId)
    {
        var id = _context.Carmodels
            .Where(c => c.Id == modelId)
            .Select(c => c.Brandid)
            .FirstOrDefault();
        return id;
    }

    public string GetBrandNameById(int id)
    {
        var data = _context.Cars
            .Where(c => c.Id == id)
            .Select(c => c.Make)
            .FirstOrDefault();
        return data;
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