using Kata.DAL.Data;

namespace Kata.DAL;

public interface IBoatRepository
{
    IEnumerable<BoatData> Get();
}
