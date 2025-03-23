using Kata.DAL;
using Kata.DAL.Models;

namespace Kata.Tests;

public class FakeBarRepository : IBarRepository
{
    private readonly IEnumerable<Bar> _bars;

    public FakeBarRepository(Bar[] bars)
    {
        _bars = bars;
    }

    public IEnumerable<Bar> Get()
    {
        return _bars;
    }
}
