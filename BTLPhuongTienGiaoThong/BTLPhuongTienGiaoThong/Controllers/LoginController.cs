using BTLPhuongTienGiaoThong.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BTLPhuongTienGiaoThong.Controllers
{
    public class LoginController : Controller
    {
        HireCarContext db = new HireCarContext();
        public IActionResult Index()
        {
            return View();
        }

        /*public IActionResult TestDatabaseConnection()
        {
            var canConnect = db.Database.CanConnect();
            if (canConnect)
            {
                return Content("Connection successful!");
            }
            else
            {
                return Content("Connection failed!");
            }
        }
        */
        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            ViewBag.IUserRoleId = new SelectList(db.TblRoles.ToList(), "IRoleId", "SRoleName");
            return View();

        }

        [HttpPost]
        [Route("register")]
        [ValidateAntiForgeryToken]
        public IActionResult Register([Bind("Username","Password","IUserRoleId","Email","FullName","PhoneNumber","Address","DateOfBirth")] TblUser user)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem tên người dùng đã tồn tại chưa
                var existingUser = db.TblUsers.FirstOrDefault(u => u.Username == user.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Username", "Tên người dùng đã tồn tại, vui lòng chọn tên khác.");
                    return View(user);
                }
                db.TblUsers.Add(user);
                db.SaveChanges();
                return RedirectToAction("login"); // Redirect to home page or another page after successful registration
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(TblUser User)
        {
            ViewBag.ErrorMessage = "";
            var user = db.TblUsers.Where(x => x.Username == User.Username && x.Password == User.Password).SingleOrDefault();

            if (user != null)
            {

                HttpContext.Session.SetString("UserName", user.Username.ToString());
                HttpContext.Session.SetInt32("UserRole", user.IUserRoleId);

                if (user.IUserRoleId == 1)
                {
                     return RedirectToAction("homeadmin", "admin");

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
                
            }
            else
            {
                ViewBag.ErrorMessage = "Tài khoản hoặc mật khẩu sai";
            }

            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Index","Home");
        }
    }
}
