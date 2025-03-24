namespace Kata.Infrastructure;

public interface IBarRepository
{
    IEnumerable<BarData> Get();
}
