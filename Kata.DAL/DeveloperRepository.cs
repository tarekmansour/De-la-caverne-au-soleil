using System.Text.Json;
using Kata.DAL.Models;

namespace Kata.DAL;

public class DeveloperRepository : IDeveloperRepository
{
    public IEnumerable<Developer> Get()
    {
        var json = File.ReadAllText("../Kata.DAL/FakeData/developers.json");
        var devs = JsonSerializer.Deserialize<IEnumerable<Developer>>(json);

        return devs;
    }
}
