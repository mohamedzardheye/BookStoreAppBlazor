using AutoMapper;
using BookStoreApp.Api.Data;
using BookStoreApp.Api.Models.booking;
using BookStoreApp.Api.Static;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BookStoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper mapper;
        private readonly ILogger<BookingController> logger;

        public BookingController(BookStoreDbContext context, IMapper mapper, ILogger<BookingController> logger)
        {
            _context = context;
            this.mapper = mapper;
            this.logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<booking_registration_read_only>>> GetBooking()
        {

          
                var books = await _context.booking_registration.OrderByDescending(x => x.id).Take(100).ToListAsync();
                var bookDtos = mapper.Map<IEnumerable<booking_registration_read_only>>(books);
                return Ok(bookDtos);
           

        }



        [HttpGet("routes")]
        public async Task<ActionResult<IEnumerable<booking_routes>>> GetRoutes()
        {


            var books = await _context.booking_routes.OrderByDescending(x => x.route_id).Take(100).ToListAsync();
            var bookDtos = mapper.Map<IEnumerable<booking_routes>>(books);
            return Ok(bookDtos);


        }

        [HttpPost]
        public async Task<ActionResult<booking_registration>> PostBooking(booking_registration booking)
        {
            try
            {
                booking.created_on = DateTime.Now;
                _context.booking_registration.Add(booking);
                await _context.SaveChangesAsync();
                return Ok(booking);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error Performing Post{nameof(PostBooking)}");
                return StatusCode(500, ex);
            }
        }




        [HttpGet("api/Bookings/GetPassengerTrips")]
        public async Task<ActionResult<IEnumerable<booking_registration_read_only>>> GetPassengerTrips(string passengerphone)

        {
            int? phone = int.Parse(passengerphone);
            try
            {
                var books = await _context.booking_registration
                    .Where(m => m.PassengerPhoneNumber == phone || m.PassengerPhoneNumber == phone)
                    .OrderByDescending(x => x.id)
                    .Take(100)
                    .ToListAsync();
                var bookDtos = mapper.Map<IEnumerable<booking_registration_read_only>>(books);

                if (bookDtos == null)
                {
                    return NotFound();
                }

                return Ok(bookDtos);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error Performing Post{nameof(PostBooking)}");
                return StatusCode(500, ex);
            }
        }

    }
}
