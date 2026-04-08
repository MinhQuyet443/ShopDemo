using Microsoft.AspNetCore.Mvc;
using ShopDemo.Models;
using System.Linq;

public class ProductController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly AppDbContext _context;

    public ProductController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var list = _context.SanPhams.ToList();
        return View(list);
    }
}