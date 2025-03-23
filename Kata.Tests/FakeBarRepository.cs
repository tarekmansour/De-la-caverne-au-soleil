using Kata.DAL;
using Kata.DAL.Data;

namespace Kata.Tests;

public class FakeBarRepository : IBarRepository
{
    private readonly IEnumerable<BarData> _bars;

    public FakeBarRepository(BarData[] bars)
    {
        _bars = bars;
    }

    public IEnumerable<BarData> Get()
    {
        return _bars;
    }
}
