namespace Kata.Infrastructure;

public interface IBoatRepository
{
    IEnumerable<BoatData> Get();
}
