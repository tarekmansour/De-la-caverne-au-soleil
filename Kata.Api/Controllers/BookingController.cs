using Kata.Domain;
using Kata.Domain.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Kata.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly MakeABooking _makeABooking;
    private readonly IBookingQueryRepository bookingRepository;

    public BookingController(MakeABooking makeABooking, IBookingQueryRepository bookingRepository)
    {
        this._makeABooking = makeABooking;
        this.bookingRepository = bookingRepository;
    }

    [HttpPut]
    public bool MakeBooking()
    {
        var booking = _makeABooking.Make();
        return booking.GetType() != typeof(BookingNotFound);
    }

    [HttpGet]
    public IEnumerable<Booking> Get()
    {
        return bookingRepository.GetUpcomingBookings();
    }
}
