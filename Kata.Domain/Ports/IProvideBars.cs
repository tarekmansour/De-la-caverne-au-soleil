namespace Kata.Domain.Ports;

public interface IProvideBars
{
    public IEnumerable<Bar> GetAll();
}
