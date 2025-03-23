namespace Kata.DAL.Models;

public record Developer
{
    public string Name { get; set; }
    public DateTime[] OnSite { get; set; }
}
