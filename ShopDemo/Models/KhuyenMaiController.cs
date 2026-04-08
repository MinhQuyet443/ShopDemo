using Microsoft.AspNetCore.Mvc;

namespace ShopDemo.Models
{
    public class KhuyenMaiController : Controller
    {
        private readonly AppDbContext _context;

        public KhuyenMaiController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var list = _context.KhuyenMais.ToList();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(KhuyenMai km)
        {
            _context.KhuyenMais.Add(km);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
