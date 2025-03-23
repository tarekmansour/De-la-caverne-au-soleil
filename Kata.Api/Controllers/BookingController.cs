using Kata.BLL;
using Kata.DAL;
using Kata.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kata.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookingService _bookingService;
        private readonly IBookingRepository _bookingRepository;

        public BookingController(BookingService bookingService, IBookingRepository bookingRepository)
        {
            _bookingService = bookingService;
            _bookingRepository = bookingRepository;
        }

        [HttpPut]
        public bool MakeBooking()
        {
            return _bookingService.ReserveBar();
        }

        [HttpGet]
        public IEnumerable<Booking> Get()
        {
            return _bookingRepository.GetUpcomingBookings();
        }
    }
}
