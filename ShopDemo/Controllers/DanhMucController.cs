using Microsoft.AspNetCore.Mvc;
using ShopDemo.Models;

namespace ShopDemo.Controllers
{
    public class DanhMucController : Controller
    {
        private readonly AppDbContext _context;

        public DanhMucController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var list = _context.DanhMucs.ToList();
            return View(list);
        }
    }
}
