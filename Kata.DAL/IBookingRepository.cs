using Kata.DAL.Data;

namespace Kata.DAL;

public interface IBookingRepository
{
    IEnumerable<BookingData> GetUpcomingBookings();
    void Save(BookingData booking);
}
