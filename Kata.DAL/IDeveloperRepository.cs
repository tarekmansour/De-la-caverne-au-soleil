using Kata.DAL.Models;

namespace Kata.DAL;

public interface IDeveloperRepository
{
    IEnumerable<Developer> Get();
}