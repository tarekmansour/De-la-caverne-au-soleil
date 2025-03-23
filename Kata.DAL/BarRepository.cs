using System.Text.Json;
using Kata.DAL.Data;

namespace Kata.DAL;

public class BarRepository : IBarRepository
{
    public IEnumerable<BarData> Get()
    {
        var json = File.ReadAllText("../Kata.DAL/FakeData/bars.json");
        var bars = JsonSerializer.Deserialize<IEnumerable<BarData>>(json);

        return bars;
    }
}
