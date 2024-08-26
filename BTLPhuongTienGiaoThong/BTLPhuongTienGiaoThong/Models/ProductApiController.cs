using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BTLPhuongTienGiaoThong.Models;
namespace BTLPhuongTienGiaoThong.Models.ProductModels;

[Route("api/[controller]")]
[ApiController]
public class ProductApiController : ControllerBase
{
    HireCarContext db = new HireCarContext(); 
    [HttpGet]   
    
    public IEnumerable<product> GetAllProducts() 
    {
        var sanpham =(from p  in db.TblCars select new product
        {
            CarId = p.CarId,
            CarMake = p.CarMake,
            CarModel = p.CarModel,
            LicensePlate = p.LicensePlate,  
            Color = p.Color,
            FuelType = p.FuelType,
            ImageUrl = p.ImageUrl,  
            PricePerDay = p.PricePerDay,    
            Rating = p.Rating,
        }).ToList();
        return sanpham;
    }
    [HttpGet("{mahang}")]
    public IEnumerable<product> GetProductsByHang(int mahang)
    {
        var sanpham = (from p in db.TblCars where p.CarMake ==mahang
                       select new product
                       {
                           CarId = p.CarId,
                           CarMake = p.CarMake,
                           CarModel = p.CarModel,
                           LicensePlate = p.LicensePlate,
                           Color = p.Color,
                           FuelType = p.FuelType,
                           ImageUrl = p.ImageUrl,
                           PricePerDay = p.PricePerDay,
                           Rating = p.Rating,
                       }).ToList();
        return sanpham;
    }
}