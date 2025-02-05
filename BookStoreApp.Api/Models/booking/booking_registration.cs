namespace BookStoreApp.Api.Models.booking
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public partial class booking_registration
    {
        public int id { get; set; }

        [StringLength(50)]
        public string? Departure { get; set; }

        public DateTime? departureTime { get; set; }

        [StringLength(50)]
        public string? Arrival { get; set; }

        public DateTime? arrivalTime { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DepartureDate { get; set; }

        [StringLength(50)]
        public string? Bus_name { get; set; }




        [StringLength(300)]
        public string? Seats { get; set; }

        public string? UpdateSeats { get; set; }

        [StringLength(50)]
        public string? PassengerName { get; set; }

        public int? PassengerPhoneNumber { get; set; }

        [StringLength(50)]
        public string? Address { get; set; }

        [Column(TypeName = "text")]
        public string? Description { get; set; }

        [StringLength(50)]
        public string? TaxiFare { get; set; }

        [StringLength(50)]
        public string? PaymentType { get; set; }

        [StringLength(50)]
        public string? CurrencyType { get; set; }

        [Column(TypeName = "decimal(18,2)")]  // Set precision and scale here
        public decimal Amount { get; set; }

        [Column(TypeName = "decimal(18,2)")]  // Set precision and scale here
        public decimal TotalAmount { get; set; }
        public string? messageType { get; set; }

        public DateTime? created_on { get; set; }

        [StringLength(50)]
        public string? created_by { get; set; }

        public int? TripStatus { get; set; }

        [StringLength(50)]
        public string? bookingId { get; set; }

        public int? route_id { get; set; }

        public bool isTransit { get; set; }
        public string? transitCity { get; set; }
    }
}
