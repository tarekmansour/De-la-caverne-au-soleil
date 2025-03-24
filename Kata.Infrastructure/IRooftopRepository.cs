namespace Kata.Infrastructure;

public interface IRooftopRepository
{
    IEnumerable<RooftopData> Get();
}
