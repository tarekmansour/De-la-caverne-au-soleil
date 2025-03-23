using Kata.DAL;
using Kata.DAL.Data;

namespace Kata.Tests;

public class FakeBookingRepository : IBookingRepository
{
    private readonly List<BookingData> _bookings = new();

    public IEnumerable<BookingData> GetUpcomingBookings()
    {
        return _bookings;
    }

    public BookingData GetUpcomingBooking(DateTime date)
    {
        return _bookings.First(b => b.Date == date);
    }

    public void Save(BookingData booking)
    {
        _bookings.Add(booking);
    }
}
