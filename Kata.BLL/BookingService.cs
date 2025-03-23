using Kata.DAL;
using Kata.DAL.Models;

namespace Kata.BLL;

public class BookingService
{
    private readonly IBarRepository _barRepository;
    private readonly IDeveloperRepository _developerRepository;
    private readonly IBookingRepository _bookingRepository;

    public BookingService(
        IBarRepository barRepository,
        IDeveloperRepository devRepository,
        IBookingRepository bookingRepository)
    {
        _barRepository = barRepository;
        _developerRepository = devRepository;
        _bookingRepository = bookingRepository;
    }

    public bool ReserveBar()
    {
        //chercher des bars et des développeurs
        var bars = _barRepository.Get();
        var devs = _developerRepository.Get().ToList();

        //chercher le nombre de développeurs disponible à une date
        var numberOfAvailableDevsByDate = new Dictionary<DateTime, int>();
        foreach (var dev in devs)
        {
            foreach (var date in dev.OnSite)
            {
                if (numberOfAvailableDevsByDate.ContainsKey(date))
                {
                    numberOfAvailableDevsByDate[date]++;
                }
                else
                {
                    numberOfAvailableDevsByDate.Add(date, 1);
                }
            }
        }

        //prendre le nombre de développeurs maximum à un date donnée: reserver le bar quand le maximum de personnes est disponible
        var maxNumberOfDevs = numberOfAvailableDevsByDate.Values.Max();

        //régle à respecter: il faut au moins 60% des developpeurs qui soit disponible pour faire la reservation, sinon on reserve pas
        if (maxNumberOfDevs <= devs.Count() * 0.6)
        {
            return false;
        }

        var bestDate = numberOfAvailableDevsByDate.First(kv => kv.Value == maxNumberOfDevs).Key;

        //chercher le bar qui a la capacité de recevori les dev et ouvert à une date
        foreach (var bar in bars)
        {
            if (bar.Capacity >= maxNumberOfDevs && bar.Open.Contains(bestDate.DayOfWeek))
            {
                //reserver le bar
                BookBar(bar.Name, bestDate);
                _bookingRepository.Save(new Booking() { Bar = bar, Date = bestDate });
                return true;
            }
        }

        return false;
    }

    private void BookBar(string name, DateTime dateTime)
    {
        Console.WriteLine("Bar booked: " + name + " at " + dateTime);
    }
}
