namespace Kata.Infrastructure;

public interface IBookingRepository
{
    IEnumerable<BookingData> GetUpcomingBookings();
    void Save(BookingData booking);
}
