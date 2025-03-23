using Kata.DAL;
using Kata.DAL.Models;

namespace Kata.Tests;


public class FakeDeveloperRepository : IDeveloperRepository
{
    private readonly IEnumerable<Developer> _developers;

    public FakeDeveloperRepository(Developer[] developers)
    {
        _developers = developers;
    }

    public IEnumerable<Developer> Get()
    {
        return _developers;
    }
}
