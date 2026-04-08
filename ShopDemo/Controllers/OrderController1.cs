using Microsoft.AspNetCore.Mvc;
using ShopDemo.Models;
using System.Linq;

namespace ShopDemo.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // 🛒 THANH TOÁNMMMM
        [HttpPost]
        public IActionResult Checkout(List<int> selectedItems, Dictionary<int, int> quantities, string paymentMethod)
        {
            int MaKH = 1;

            // ❌ nếu không chọn gì
            if (selectedItems == null || selectedItems.Count == 0)
            {
                return RedirectToAction("Index", "Cart");
            }

            // 🛒 lấy sản phẩm đã tick
            var cart = _context.GioHangs
                .Where(x => x.MaKH == MaKH && selectedItems.Contains(x.MaSP))
                .ToList();
            if (paymentMethod == "ONLINE")
            {
                return RedirectToAction("Payment");
            }

            // 🧾 tạo đơn hàng
            var donHang = new DonHang
            {
                MaKH = MaKH,
                NgayTao = DateTime.Now,
                HinhThucTT = paymentMethod,
                TrangThai = "Chờ xử lý"
            };

            _context.DonHangs.Add(donHang);
            _context.SaveChanges();

            // 🧾 chi tiết đơn
            foreach (var item in cart)
            {
                int soluong = quantities[item.MaSP];

                _context.ChiTietDonHangs.Add(new ChiTietDonHang
                {
                    MaDH = donHang.MaDH,
                    MaSP = item.MaSP,
                    SoLuong = soluong,
                    TongTien = soluong * item.DonGia
                });
            }

            _context.SaveChanges();

            // ❌ xóa giỏ
            _context.GioHangs.RemoveRange(cart);
            _context.SaveChanges();

            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return Content("✅ Đặt hàng thành công!");
        }
        public IActionResult Payment()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ConfirmPayment()
        {
            int MaKH = 1;

            // 👉 Lấy đơn hàng mới nhất của khách
            var donHang = _context.DonHangs
                .Where(x => x.MaKH == MaKH)
                .OrderByDescending(x => x.MaDH)
                .FirstOrDefault();

            if (donHang != null)
            {
                donHang.TrangThai = "Đã thanh toán";
                _context.SaveChanges();
            }

            return RedirectToAction("Success");
        }

    }
}