namespace BTLPhuongTienGiaoThong.Models.ProductModels
{
    public class product
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

        public string? FuelType { get; set; }
    }
}
