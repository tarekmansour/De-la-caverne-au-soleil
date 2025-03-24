using Kata.Domain;

namespace Kata.Api.Controllers;

public interface IBookingQueryRepository
{
    IEnumerable<Booking> GetUpcomingBookings();
}
