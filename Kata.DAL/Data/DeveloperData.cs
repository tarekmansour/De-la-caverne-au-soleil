namespace Kata.DAL.Data;

public record DeveloperData
{
    public string Name { get; set; }
    public DateTime[] OnSite { get; set; }
}
