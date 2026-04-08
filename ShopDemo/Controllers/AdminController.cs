using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopDemo.Models;
using System.Linq;

public class AdminController : Controller
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        _context = context;
    }

    // 📦 DANH SÁCH
    public IActionResult Index()
    {
        var list = _context.SanPhams.ToList();
        return View(list);
    }

    // ➕ FORM THÊM
    public IActionResult Create()
    {
        ViewBag.DanhMuc = new SelectList(_context.DanhMucs, "MaDM", "TenDM");
        return View();
    }

    // ➕ XỬ LÝ THÊM
    [HttpPost]
    public IActionResult Create(SanPham sp)
    {
        _context.SanPhams.Add(sp);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    // ✏️ FORM SỬA
    public IActionResult Edit(int id)
    {
        var sp = _context.SanPhams.Find(id);
        ViewBag.DanhMuc = new SelectList(_context.DanhMucs, "MaDM", "TenDM", sp.MaDM);
        return View(sp);
    }

    // ✏️ XỬ LÝ SỬA
    [HttpPost]
    public IActionResult Edit(SanPham sp)
    {
        _context.SanPhams.Update(sp);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    // ❌ XÓA
    public IActionResult Delete(int id)
    {
        var sp = _context.SanPhams.Find(id);
        _context.SanPhams.Remove(sp);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}