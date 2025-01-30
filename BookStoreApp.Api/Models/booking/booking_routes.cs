namespace Sahal_Projects.Areas.APIs.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class booking_routes
    {
        [Key]
        public int route_id { get; set; }

        [StringLength(50)]
        public string? bus_name { get; set; }

        [StringLength(50)]
        public required string board_point { get; set; }

        public DateTime? board_time { get; set; }

        [StringLength(50)]
        public string? drop_point { get; set; }

        public DateTime? drop_time { get; set; }

        [Column(TypeName = "decimal(18,2)")]  // Explicitly specify the column type with precision and scale
 
        public decimal? price { get; set; }

        [StringLength(50)]
        public string? CurrencyType { get; set; }

        [StringLength(50)]
        public string? created_by { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? created_on { get; set; }

        [Column(TypeName = "date")]
        public DateTime? created_date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? route_date { get; set; }

        public string? Status { get; set; }

        public bool isTransit { get; set; }

        public string? transitCity { get; set; }

        public DateTime? board_time_transit { get; set; }
    }
}
