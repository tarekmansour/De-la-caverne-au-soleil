namespace Kata.BLL;

public class BookingNotFound : Booking
{
    public BookingNotFound() : base(DateTime.MinValue, new BarNotFound())
    {
    }
}
