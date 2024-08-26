using System;
using System.Collections.Generic;

namespace BTLPhuongTienGiaoThong.Models;

public partial class TblBooking
{
    public int BookingId { get; set; }

    public int UserBookingId { get; set; }

    public int CarBookingId { get; set; }

    public DateTime BookingDate { get; set; }

    public DateTime RentalStartDate { get; set; }

    public DateTime RentalEndDate { get; set; }

    public decimal TotalCost { get; set; }

    public string StatusCar { get; set; } = null!;
}
