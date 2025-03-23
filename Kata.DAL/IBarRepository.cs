using Kata.DAL.Models;

namespace Kata.DAL;

public interface IBarRepository
{
    IEnumerable<Bar> Get();
}
