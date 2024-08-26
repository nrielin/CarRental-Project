using BTLPhuongTienGiaoThong.Models;
using BTLPhuongTienGiaoThong.Models.Authentication;
using BTLPhuongTienGiaoThong.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;

namespace BTLPhuongTienGiaoThong.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        HireCarContext db = new HireCarContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        
        public IActionResult Index(int? page)
        {
            //chia danh sách xe ra thành các trang nh?
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lstXe = db.TblCars.AsNoTracking().OrderBy(x => x.CarId);
            PagedList<TblCar> lst = new PagedList<TblCar>(lstXe, pageNumber, pageSize);
            return View(lst);
        }
        public IActionResult XeTheoHang(int CarMake, int? page)
        {//chia danh sách xe ra thành các trang nh?
         //chia danh sách xe ra thành các trang nh?
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lstXe = db.TblCars.AsNoTracking().Where(x=>x.CarMake==CarMake).OrderBy(x => x.CarId);
            PagedList<TblCar> lst = new PagedList<TblCar>(lstXe, pageNumber, pageSize);
            ViewBag.CarMake = CarMake;
            return View(lst);
        }
        public IActionResult ChiTietXe(int CarID)
        {
            var xe = db.TblCars.SingleOrDefault(x => x.CarId == CarID);
            var xecarmake = db.TblHangXes.SingleOrDefault(x => x.CarMake == xe.CarMake);
            ViewBag.xeCarMake = xecarmake.NameMake;
            return View(xe);
         }
        public IActionResult ProductDetail(int CarID)
        {
            var xe = db.TblCars.SingleOrDefault(x => x.CarId == CarID);
            var xecarmake = db.TblHangXes.SingleOrDefault(x => x.CarMake == xe.CarMake);
            var homeProductDetailViewModel = new HomeProductDetailViewModel { Car =xe,tenhangXe=xecarmake.NameMake };
            return View(homeProductDetailViewModel);
        }
        public IActionResult Privacy()
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
