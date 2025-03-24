using Kata.Domain;
using Kata.Domain.Ports;

namespace Kata.Infrastructure.Adapters;

public class DevelopersAvailabilitiesAdapter : IProvideDevelopersAvailabilities
{
    private readonly IDeveloperRepository devRepository;

    public DevelopersAvailabilitiesAdapter(IDeveloperRepository devRepository)
    {
        this.devRepository = devRepository;
    }

    public DeveloperAvailabilities Get()
    {
        var devs = devRepository.Get();
        var availabilities = new List<DeveloperAvailability>();
        foreach (var date in devs.SelectMany(dev => dev.OnSite))
        {
            var devAvailability = availabilities.FirstOrDefault(availability => availability.Date == date);
            if (devAvailability == null)
            {
                availabilities.Add(new DeveloperAvailability(date, 1));
            }
            else
            {
                var numberOfPeople = devAvailability.NumberOfPeople + 1;
                availabilities.Remove(devAvailability);
                availabilities.Add(new DeveloperAvailability(date, numberOfPeople));
            }
        }

        return new DeveloperAvailabilities(availabilities, devs.Count());
    }
}
