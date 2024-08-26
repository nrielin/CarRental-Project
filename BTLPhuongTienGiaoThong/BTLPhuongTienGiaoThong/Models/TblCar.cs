using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BTLPhuongTienGiaoThong.Models;

public partial class TblCar
{
    public int CarId { get; set; }

    public int? CarMake { get; set; }

    public string? CarModel { get; set; }

    public int? YearProduction { get; set; }

    public string? Color { get; set; }

    public float? PricePerDay { get; set; }

    public string? ImageUrl { get; set; }

    public decimal? Rating { get; set; }

    public string? LicensePlate { get; set; }

    public int? Seats { get; set; }

    public string? Transmission { get; set; }
    [Required]
    [StringLength(20, ErrorMessage = "Loại nhiên liệu không được vượt quá 20 ký tự.")]
    public string? FuelType { get; set; }

    public virtual TblHangXe? CarMakeNavigation { get; set; }
}
