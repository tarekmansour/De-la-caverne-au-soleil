using Kata.DAL.Data;

namespace Kata.DAL;

public interface IDeveloperRepository
{
    IEnumerable<DeveloperData> Get();
}