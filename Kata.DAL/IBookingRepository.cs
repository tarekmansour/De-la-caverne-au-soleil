using Kata.DAL.Models;

namespace Kata.DAL;

public interface IBookingRepository
{
    IEnumerable<Booking> GetUpcomingBookings();
    void Save(Booking booking);
}
