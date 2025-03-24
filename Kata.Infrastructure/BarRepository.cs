using System.Text.Json;

namespace Kata.Infrastructure;

public class BarRepository : IBarRepository
{
    public IEnumerable<BarData> Get()
    {
        var json = File.ReadAllText("../Kata.DAL/FakeData/bars.json");
        var bars = JsonSerializer.Deserialize<IEnumerable<BarData>>(json);

        return bars;
    }
}
