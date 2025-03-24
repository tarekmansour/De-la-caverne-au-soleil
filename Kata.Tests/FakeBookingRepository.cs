using Kata.Api.Controllers;
using Kata.Domain;
using Kata.Domain.Ports;

namespace Kata.Tests;

public class FakeBookingRepository : ISaveBooking, IBookingQueryRepository
{
    private readonly List<Booking> bookings = new();

    public void Save(Booking booking)
    {
        if (booking.GetType() != typeof(BookingNotFound))
        {
            bookings.Add(booking);
        }
    }

    public IEnumerable<Booking> GetUpcomingBookings()
    {
        return bookings;
    }
}
