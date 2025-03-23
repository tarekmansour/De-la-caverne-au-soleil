using System.Text.Json;
using Kata.DAL.Models;

namespace Kata.DAL;

public class BarRepository : IBarRepository
{
    public IEnumerable<Bar> Get()
    {
        var json = File.ReadAllText("../Kata.DAL/FakeData/bars.json");
        var bars = JsonSerializer.Deserialize<IEnumerable<Bar>>(json);

        return bars;
    }
}
