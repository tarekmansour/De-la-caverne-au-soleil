using Kata.Domain.Ports;

namespace Kata.Domain.UseCases;

public class MakeABooking
{
    private readonly ISaveBooking bookingRecorder;
    private readonly IProvideBars barProvider;
    private readonly IProvideDevelopersAvailabilities developersAvailabilitiesProvider;

    public MakeABooking(IProvideBars barProvider,
        IProvideDevelopersAvailabilities developersAvailabilitiesProvider,
        ISaveBooking bookingRecorder)
    {
        this.barProvider = barProvider;
        this.developersAvailabilitiesProvider = developersAvailabilitiesProvider;
        this.bookingRecorder = bookingRecorder;
    }

    public Booking Make()
    {
        var developersAvailabilities = developersAvailabilitiesProvider.Get();
        var bestDate = developersAvailabilities.SelectBestDate();
        if (bestDate is DateNotFound)
        {
            return new BookingNotFound();
        }

        var bars = barProvider.GetAll();
        var booking = Booking.Make(bestDate, bars);
        bookingRecorder.Save(booking);

        return booking;
    }
}
