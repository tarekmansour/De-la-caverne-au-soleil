using Kata.DAL;
using Kata.DAL.Data;

namespace Kata.Tests;

public class FakeBoatRepository : IBoatRepository
{
    private readonly IEnumerable<BoatData> _boats;

    public FakeBoatRepository(IEnumerable<BoatData> boatData)
    {
        _boats = boatData;
    }

    public IEnumerable<BoatData> Get()
    {
        return _boats;
    }
}
