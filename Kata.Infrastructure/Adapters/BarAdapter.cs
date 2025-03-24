using Kata.Domain;
using Kata.Domain.Ports;

namespace Kata.Infrastructure.Adapters;

public class BarAdapter : IProvideBars
{
    private readonly IBarRepository barRepo;
    private readonly IBoatRepository boatRepository;
    private readonly IRooftopRepository rooftopRepository;

    public BarAdapter(IBarRepository barRepo, IBoatRepository boatRepository, IRooftopRepository rooftopRepository)
    {
        this.barRepo = barRepo;
        this.boatRepository = boatRepository;
        this.rooftopRepository = rooftopRepository;
    }

    public IEnumerable<Bar> GetAll()
    {
        var indoorBars = barRepo.Get();
        var boats = boatRepository.Get();
        var allBars = GetIndoorBars(indoorBars)
            .Concat(GetBoats(boats))
            .ToList();
        return allBars;
    }


    private static IEnumerable<Bar> GetBoats(IEnumerable<BoatData> boats)
        => boats
            .Select(boat => new Bar(new BarName(boat.Name), boat.MaxPeople, Enum.GetValues<DayOfWeek>(), true));

    private static IEnumerable<Bar> GetIndoorBars(IEnumerable<BarData> indoorBars) =>
        indoorBars
            .Select(bar => new Bar(new BarName(bar.Name), bar.Capacity, bar.Open, false));
}
