using BTLPhuongTienGiaoThong.Models;
using BTLPhuongTienGiaoThong.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
namespace BTLPhuongTienGiaoThong.ViewComponents
{
    public class HangXeMenuViewComponent: ViewComponent
    {
        private readonly IHangXeRepository _hangXe;
        public HangXeMenuViewComponent(IHangXeRepository hangXeRepository)
        {
            _hangXe = hangXeRepository;
            
        }
        public IViewComponentResult Invoke()
        {
            var hangXe =_hangXe.GetAllHangXe().OrderBy(x=>x.CarMake); 
            return View(hangXe);
        }
    }
}
