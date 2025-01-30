using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.Api.Models.booking
{
    public class booking_registration_read_only
    {
        [StringLength(50)]
        public string? Departure { get; set; }


        [StringLength(50)]
        public string? Bus_name { get; set; }




        [StringLength(300)]
        public string? Seats { get; set; }

        public string? UpdateSeats { get; set; }

        [StringLength(50)]
        public string? PassengerName { get; set; }

        public int? PassengerPhoneNumber { get; set; }


    }
}
