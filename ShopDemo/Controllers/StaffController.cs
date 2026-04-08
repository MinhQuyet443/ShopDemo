using Microsoft.AspNetCore.Mvc;

namespace ShopDemo.Controllers
{
    public class StaffController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Role");

            if (role != "NhanVien")
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }
    }
}
