namespace Kata.Infrastructure;

public interface IDeveloperRepository
{
    IEnumerable<DeveloperData> Get();
}