using BTLPhuongTienGiaoThong.Models;
using BTLPhuongTienGiaoThong.Models.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BTLPhuongTienGiaoThong.Helpers;

namespace BTLPhuongTienGiaoThong.Controllers
{
    public class BookingController : Controller
    {
        private readonly HireCarContext _context;
        public BookingController(HireCarContext context)
        {
            _context = context;
        }

        HireCarContext db = new HireCarContext();
        public List<TblBooking> Carts
        {

            get
            {
                var data = HttpContext.Session.Get<List<TblBooking>>("Giohang");
                if (data == null) { data = new List<TblBooking>(); }
                return data;
            }

        }
        [Authentication]
        [HttpGet]

        public IActionResult Book(int id)
        {
            var car = db.TblCars.Find(id);
            var booking = db.TblBookings.SingleOrDefault(x => x.CarBookingId == id);
            if (car == null)
            {
                return NotFound();
            }
              // Tính số ngày thuê
            int rentalDays = (booking.RentalEndDate - booking.RentalStartDate).Days;
            var book = new TblBooking
            {
                CarBookingId = booking.CarBookingId,
                UserBookingId = id,
                RentalStartDate = booking.RentalStartDate,
                RentalEndDate = booking.RentalEndDate,
                BookingDate = DateTime.Now,
                TotalCost = rentalDays * (decimal)car.PricePerDay,
                StatusCar = "da dat"
            };
            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Book(TblBooking booking)
        {
            if (booking == null)
            {
                ModelState.AddModelError("", "Dữ liệu đặt xe không hợp lệ.");
                return View(booking);
            }

            // Kiểm tra CarId
            if (booking.CarBookingId <= 0)
            {
                ModelState.AddModelError("", "ID của xe không hợp lệ.");
                return View(booking);
            }

            var car = db.TblCars.Find(booking.CarBookingId);
            if (car == null)
            {
                ModelState.AddModelError("", "Xe không tồn tại.");
                return View(booking);
            }

            if (ModelState.IsValid)
            {

                // Kiểm tra tính khả dụng của xe
                var existingBooking = db.TblBookings
                    .Any(b => b.CarBookingId == booking.CarBookingId &&
                              b.RentalStartDate <= booking.RentalEndDate && b.RentalEndDate >= booking.RentalStartDate);

                if (existingBooking)
                {
                    ModelState.AddModelError("", "Xe đã được đặt trong khoảng thời gian này.");
                    return View(booking);
                }

                // Lấy thông tin người dùng
                var userName = HttpContext.Session.GetString("UserName"); // Thay thế GetCurrentUserName bằng phương thức thực tế của bạn
                var user = db.TblUsers.FirstOrDefault(u => u.Username == userName);

                if (user == null)
                {
                    ModelState.AddModelError("", "Không thể xác định người dùng.");
                    return View(booking);
                }
                int rentalDays = (booking.RentalEndDate - booking.RentalStartDate).Days;


                int userId = user.UserId;
                var book = new TblBooking
                {
                    CarBookingId = booking.CarBookingId,
                    UserBookingId = userId,
                    RentalStartDate = booking.RentalStartDate,
                    RentalEndDate = booking.RentalEndDate,
                    BookingDate = DateTime.Now,
                    TotalCost = (decimal)rentalDays * (decimal)car.PricePerDay,
                    StatusCar = "da dat"
                };
                try
                {
                    db.TblBookings.Add(book);
                    db.SaveChanges();

                    // Thêm thông báo thành công vào TempData
                    TempData["SuccessMessage"] = "Đặt xe thành công!";
                    return RedirectToAction("hiendsBooking");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Có lỗi xảy ra: {ex.Message}");
                    //return RedirectToAction("Book");
                    return View(book);
                }
            }
            return View(booking);
        }

        /*public IActionResult AddtoCart(int id)
        {

            var myCart = Carts;
            var item = myCart.SingleOrDefault(p => p.CarBookingId == id);
            if (item == null)//chưa có
            {

        }

        */
        public IActionResult hiendsBooking()
        {
            var userName = HttpContext.Session.GetString("UserName"); // Thay thế GetCurrentUserName bằng phương thức thực tế của bạn
            var user = db.TblUsers.FirstOrDefault(u => u.Username == userName);

            if (user == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var list = db.TblBookings.Where(x => x.UserBookingId == user.UserId).ToList();

            return View(list);
        }
        private string GetCurrentUserName()
        {
            // Giả sử bạn lưu tên người dùng trong session
            return HttpContext.Session.GetString("UserName");
        }


        // GET: Booking/Cancel/5
        public IActionResult Delete(int id)
        {
            var booking = db.TblBookings.Find(id);
            if (booking == null)
            {
                return NotFound();
            }
            db.TblBookings.Remove(booking);
            db.SaveChanges();
            //TempData["Cancel"] = "Hủy thành công!";
            return RedirectToAction("hiendsBooking");
        }
        [HttpGet]
        public IActionResult Pay(int id)
        {
            var booking = db.TblBookings.Find(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Pay(TblBooking bookingPay)
        {

            if (ModelState.IsValid)
            {
                db.Update(bookingPay);
                db.SaveChanges();
            }
            return RedirectToAction("hiendsBooking");
        }



        // POST: Booking/Cancel/5
        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        public IActionResult CancelConfirmed(int id)
        {
            var booking = db.TblBookings.Find(id);
            if (booking == null)
            {
                return NotFound();
            }

            db.TblBookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("Index", "Cars");
        }


        public IActionResult Index()
        {
            var cars = db.TblCars.ToList(); // Lấy danh sách xe từ cơ sở dữ liệu
                                            //var cars =db.TblTblBooking.ToList();
            return View(cars);
        }

        [HttpPost]
        public IActionResult CalculateTotalCost(int vehicleCode, DateTime startDate, DateTime endDate)
        {
            var car = db.TblCars.Find(vehicleCode);
  
            if (car == null)
            {
                return NotFound();
            }
            // Calculate the total cost based on the vehicle code, rental start and end dates
            var timeDiff = (endDate - startDate).TotalDays;
            var totalCost = (float)(car.PricePerDay * timeDiff );

            return Json(totalCost);
        }

        

    }
}
