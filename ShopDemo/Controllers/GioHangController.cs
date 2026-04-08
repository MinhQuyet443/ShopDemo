using Microsoft.AspNetCore.Mvc;
using ShopDemo.Models;
using System.Linq;

public class CartController : Controller
{
    private readonly AppDbContext _context;

    public CartController(AppDbContext context)
    {
        _context = context;
    }

    // ✅ THÊM VÀO GIỎ (PHẢI CÓ)
    [HttpGet]
    public JsonResult AddToCart(int MaSP)
    {
        int MaKH = 1;

        var sp = _context.SanPhams.FirstOrDefault(x => x.MaSP == MaSP);

        if (sp == null)
        {
            return Json(new { success = false });
        }

        var item = _context.GioHangs
            .FirstOrDefault(x => x.MaKH == MaKH && x.MaSP == MaSP);

        if (item != null)
        {
            item.SoLuong++;
        }
        else
        {
            _context.GioHangs.Add(new GioHang
            {
                MaKH = MaKH,
                MaSP = MaSP,
                SoLuong = 1,
                DonGia = sp.GiaTien
            });
        }

        _context.SaveChanges();

        return Json(new { success = true });
    }

    // ✅ HIỂN THỊ GIỎ HÀNG
    public IActionResult Index()
    {
        int MaKH = 1;

        var cart = (from gh in _context.GioHangs
                    join sp in _context.SanPhams
                    on gh.MaSP equals sp.MaSP
                    where gh.MaKH == MaKH
                    select new CartViewModel
                    {
                        MaSP = gh.MaSP,
                        TenSP = sp.TenSP,
                        HinhAnh = sp.HinhAnh,
                        DonGia = gh.DonGia,
                        SoLuong = gh.SoLuong
                    }).ToList();

        return View(cart);
    }
    public IActionResult Delete(int MaSP)
    {
        int MaKH = 1;

        var item = _context.GioHangs
            .FirstOrDefault(x => x.MaKH == MaKH && x.MaSP == MaSP);

        if (item != null)
        {
            _context.GioHangs.Remove(item);
            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }
}