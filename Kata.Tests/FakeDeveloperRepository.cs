using Kata.Infrastructure;

namespace Kata.Tests;


public class FakeDeveloperRepository : IDeveloperRepository
{
    private readonly IEnumerable<DeveloperData> _developers;

    public FakeDeveloperRepository(DeveloperData[] developers)
    {
        _developers = developers;
    }

    public IEnumerable<DeveloperData> Get()
    {
        return _developers;
    }
}
