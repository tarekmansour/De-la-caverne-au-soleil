using System.Text.Json;
using Kata.DAL.Data;

namespace Kata.DAL;

public class DeveloperRepository : IDeveloperRepository
{
    public IEnumerable<DeveloperData> Get()
    {
        var json = File.ReadAllText("../Kata.DAL/FakeData/developers.json");
        var devs = JsonSerializer.Deserialize<IEnumerable<DeveloperData>>(json);

        return devs;
    }
}
