namespace Kata.Domain;

public class DeveloperAvailabilities
{
    private readonly IEnumerable<DeveloperAvailability> availabilities;
    private readonly int totalNumberOfDevelopers;

    private const double MinimumPercentageOfDevs = 0.6;

    public DeveloperAvailabilities(IEnumerable<DeveloperAvailability> availabilities, int totalNumberOfDevelopers)
    {
        this.availabilities = availabilities;
        this.totalNumberOfDevelopers = totalNumberOfDevelopers;
    }

    public BestDate SelectBestDate()
    {
        var maximumOfDevsOnSite = availabilities.Select(availability => availability.NumberOfPeople.Value)
            .Max();

        if (AMinimumOfDevIsNotAvailable(totalNumberOfDevelopers, maximumOfDevsOnSite))
        {
            return new DateNotFound();
        }

        var favoriteDate = availabilities
            .First(devAvailability => devAvailability.NumberOfPeople == maximumOfDevsOnSite)
            .Date;

        return new BestDate(favoriteDate, maximumOfDevsOnSite);
    }
    private static bool AMinimumOfDevIsNotAvailable(int totalNumberOfDevelopers, int maxNumberOfDevsAvailable)
    {
        return maxNumberOfDevsAvailable <= totalNumberOfDevelopers * MinimumPercentageOfDevs;
    }
}
