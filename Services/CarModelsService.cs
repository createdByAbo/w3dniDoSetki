using dotenv.net;
using DotNetEnv;
using Newtonsoft.Json;
using w3dniDoSetki.Exceptions;

namespace w3dniDoSetki.Services;

public interface ICarModelsService
{
    string WriteModelsToJson();
}
public class CarModelsService : ICarModelsService
{
    private readonly W3dnidosetkiContext _context;

    public CarModelsService(W3dnidosetkiContext context)
    {
        _context = context;
    }

    public string WriteModelsToJson()
    {
        List<List<string>> data = new List<List<string>>();
        int x = Int32.MaxValue;
        for (int i = 1; i <= _context.Carmodels.Max( cm => cm.Brandid) + 1; i++)
        {
            List<string> models = new List<string>();
            var json = _context.Carmodels
                .Where(cm => cm.Brandid == i)
                .ToList();
            for (int j = 0; j < json.Count; j++)
            {
                models.Add(json[j].Model);
            }

            List<string> modelswb = new List<string>();
            modelswb.Add($"{i}");
            modelswb.Add(JsonConvert.SerializeObject(models).ToString());
            data.Add(modelswb);
        }
        File.WriteAllText("./JSON/models.json", JsonConvert.SerializeObject(data).Replace(@"\", ""));
        return "";
    }
}