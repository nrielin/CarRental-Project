using BTLPhuongTienGiaoThong.Models;
using BTLPhuongTienGiaoThong.Models.Authentication;


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BTLPhuongTienGiaoThong.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    [Authentication]
    public class HomeAdminController : Controller
    {
        
        HireCarContext db = new HireCarContext();
        
        [Route("")]
        [Route("index")]
        
        public IActionResult Index()
        {
            return View();
        }
        [Route("danhmucxe")]
        public IActionResult DanhMucXe(int? page)
        {
            //chia danh sách xe ra thành các trang nh?
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lstXe = db.TblCars.AsNoTracking().OrderBy(x => x.CarId);
            PagedList<TblCar> lst = new PagedList<TblCar>(lstXe, pageNumber, pageSize);
            return View(lst);
        }
        [Route("ThemXe")]
        [HttpGet]
        public IActionResult ThemXe()
        {
            ViewBag.CarMake = new SelectList(db.TblHangXes.ToList(), "CarMake", "NameMake");
            return View();
        }
        [Route("ThemXe")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemXe([Bind("CarMake,CarModel,YearPrroduction,Color,PicePerDay,Rating,LicensePlate,Seats,Transmission,FuelType,ImageUrl")] TblCar car)
        {
            if (ModelState.IsValid)
            {

                db.TblCars.Add(car);
                db.SaveChanges();
                return RedirectToAction("DanhMucXe");
            }
            return View(car);
        }
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            var item = db.TblCars.Find(id);
            db.TblCars.Remove(item);
            db.SaveChanges();   
            return RedirectToAction("DanhMucXe");
        }
        [Route("edit")]
        [HttpGet]
        public IActionResult Edit(int id)
        {

            ViewBag.CarMake = new SelectList(db.TblHangXes.ToList(), "CarMake", "NameMake");
            var car = db.TblCars.Find(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }
        [Route("edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TblCar car)
        {
            if (ModelState.IsValid)
            {
               // db.Update(car);
               db.Entry(car).State =EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("danhmucxe", "homeadmin");
            }
            return View(car);
        }
    }
}
