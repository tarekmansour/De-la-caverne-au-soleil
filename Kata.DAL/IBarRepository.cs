using Kata.DAL.Data;

namespace Kata.DAL;

public interface IBarRepository
{
    IEnumerable<BarData> Get();
}
