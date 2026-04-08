using Microsoft.AspNetCore.Mvc;
using ShopDemo.Models;
using System.Linq;

public class AccountController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly AppDbContext _context;

    public AccountController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Hiển thị form đăng ký
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    // POST: Xử lý đăng ký
    [HttpPost]
    public IActionResult Register(KhachHang model)
    {
        if (model == null)
        {
            return View();
        }

        if (ModelState.IsValid)
        {
            _context.KhachHangs.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        // trả lại dữ liệu nếu lỗi
        return View(model);

        if (_context.KhachHangs.Any(x => x.Username == model.Username))
        {
            ViewBag.Error = "Username đã tồn tại!";
            return View();
        }

        _context.KhachHangs.Add(model);
        _context.SaveChanges();

        return RedirectToAction("Login");
    }
    // GET
    public IActionResult Login()
    {
        return View();
    }

    // POST
    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        // 1. Check KhachHang
        var kh = _context.KhachHangs
            .FirstOrDefault(x => x.Username == model.Username
                              && x.Password == model.Password);

        if (kh != null)
        {
            HttpContext.Session.SetString("Role", "KhachHang");
            HttpContext.Session.SetString("Username", kh.Username);
            HttpContext.Session.SetInt32("MaKH", kh.MaKH);

            return RedirectToAction("Index", "Home");
        }

        // 2. Check NhanVien
        var nv = _context.NhanViens
            .FirstOrDefault(x => x.Username == model.Username
                              && x.Password == model.Password);

        if (nv != null)
        {
            HttpContext.Session.SetString("Role", "NhanVien");
            HttpContext.Session.SetString("Username", nv.Username ?? "");

            // 🔥 THÊM 2 DÒNG NÀY
            HttpContext.Session.SetInt32("MaQuyen", nv.MaQuyen);
            HttpContext.Session.SetInt32("MaNV", nv.MaNV);

            return RedirectToAction("Index", "Admin");
        }

        ViewBag.Error = "Sai tài khoản!";
        return View();
    }
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

}