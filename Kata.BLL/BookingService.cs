using Kata.DAL;
using Kata.DAL.Data;

namespace Kata.BLL;

public class BookingService
{
    private readonly IBarRepository _barRepository;
    private readonly IDeveloperRepository _developerRepository;
    private readonly IBoatRepository _boatRepository;
    private readonly IBookingRepository _bookingRepository;

    public BookingService(
        IBarRepository barRepository,
        IDeveloperRepository devRepository,
        IBoatRepository boatRepository,
        IBookingRepository bookingRepository)
    {
        _barRepository = barRepository;
        _developerRepository = devRepository;
        _boatRepository = boatRepository;
        _bookingRepository = bookingRepository;
    }

    public bool ReserveBar()
    {
        //chercher des bars et des développeurs
        var bars = _barRepository.Get();
        var boats = _boatRepository.Get();
        var devs = _developerRepository.Get().ToList();

        var allBars = GetAllBars(bars, boats);
        var availabilities = GetAvailabilities(devs);

        var bestDate = BestDate.Get(availabilities, devs.Count);
        var booking = Booking.Make(bestDate, allBars);

        if (booking is not BookingNotFound)
        {
            _bookingRepository.Save(new BookingData()
            {
                Bar = new BarData(booking.Bar.Name, booking.Bar.Capacity, booking.Bar.OpenDays),
                Date = booking.Date
            });
            return true;
        }

        return false;
    }

    private IEnumerable<Bar> GetAllBars(IEnumerable<BarData> bars, IEnumerable<BoatData> boats)
    {
        IEnumerable<Bar> allBars = bars
            .Select(b => new Bar(b.Name, b.Capacity, b.Open, false))
            .Concat(boats.Select(b => new Bar(b.Name, b.MaxPeople, Enum.GetValues<DayOfWeek>(), true)));

        return allBars;
    }

    private List<DeveloperAvailability> GetAvailabilities(List<DeveloperData> devs)
    {
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
        return availabilities;
    }
}
