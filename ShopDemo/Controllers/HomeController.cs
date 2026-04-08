using Microsoft.AspNetCore.Mvc;
using ShopDemo.Models;
using System.Linq;

public class HomeController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(string search, int? madm)
    {
        // Lấy danh mục
        var danhmucs = _context.DanhMucs.ToList();
        ViewBag.DanhMucs = danhmucs;

        // Lấy sản phẩm (group)
        var sanphams = _context.SanPhams
            .GroupBy(x => x.TenSP)
            .Select(g => g.First())
            .ToList();

        // 🔍 TÌM KIẾM
        if (!string.IsNullOrEmpty(search))
        {
            sanphams = sanphams
                .Where(x => x.TenSP.Contains(search))
                .ToList(); // 👈 QUAN TRỌNG
        }

        // 📂 LỌC DANH MỤC
        if (madm != null)
        {
            sanphams = sanphams
                .Where(x => x.MaDM == madm)
                .ToList(); // 👈 QUAN TRỌNG
        }

        return View(sanphams);
    }
}