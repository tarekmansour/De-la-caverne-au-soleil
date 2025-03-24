namespace Kata.Domain;

public class BookingNotFound : Booking
{
    public BookingNotFound() : base(DateTime.MinValue, new BarNotFound())
    {
    }
}
