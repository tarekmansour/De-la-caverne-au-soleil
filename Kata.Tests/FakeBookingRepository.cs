using Kata.DAL;
using Kata.DAL.Models;

namespace Kata.Tests;

public class FakeBookingRepository : IBookingRepository
{
    private readonly List<Booking> _bookings = new();

    public IEnumerable<Booking> GetUpcomingBookings()
    {
        return _bookings;
    }

    public Booking GetUpcomingBooking(DateTime date)
    {
        return _bookings.First(b => b.Date == date);
    }

    public void Save(Booking booking)
    {
        _bookings.Add(booking);
    }
}
