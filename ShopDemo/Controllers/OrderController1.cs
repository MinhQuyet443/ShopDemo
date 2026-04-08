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
        // ⚡ MUA NGAY
        [HttpPost]
        public IActionResult BuyNow(int productId)
        {
            return RedirectToAction("CheckoutNow", new { id = productId });
        }

        // 📄 TRANG NHẬP THÔNG TIN
        public IActionResult CheckoutNow(int id)
        {
            var sp = _context.SanPhams.Find(id);
            return View(sp);
        }

        // 💾 LƯU ĐƠN HÀNG
        [HttpPost]
        public IActionResult PlaceOrderNow(int productId, string phone, string address, string paymentMethod)
        {
            int MaKH = 1;

            var sp = _context.SanPhams.Find(productId);
            if (sp == null) return NotFound();

            var donHang = new DonHang
            {
                MaKH = MaKH,
                NgayTao = DateTime.Now,
                HinhThucTT = paymentMethod,
                TrangThai = paymentMethod == "ONLINE" ? "Chờ xác nhận" : "Chờ xử lý",
                SoDienThoai = phone,
                DiaChi = address
            };

            _context.DonHangs.Add(donHang);
            _context.SaveChanges();

            _context.ChiTietDonHangs.Add(new ChiTietDonHang
            {
                MaDH = donHang.MaDH,
                MaSP = sp.MaSP,
                SoLuong = 1,
                TongTien = sp.GiaTien
            });

            _context.SaveChanges();

            if (paymentMethod == "ONLINE")
            {
                return RedirectToAction("Payment");
            }

            return Content("<script>alert('Đặt hàng thành công!'); window.location.href='/'</script>", "text/html; charset=utf-8");
        }
        public IActionResult CheckoutCart()
        {
            if (TempData["SelectedItems"] == null)
                return RedirectToAction("Index", "Cart");

            var ids = TempData["SelectedItems"].ToString()
                .Split(',')
                .Select(int.Parse)
                .ToList();

            int MaKH = 1;

            var cart = _context.GioHangs
                .Where(x => x.MaKH == MaKH && ids.Contains(x.MaSP))
                .ToList();

            return View(cart);
        }
        // 🛒 THANH TOÁN
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
            TempData["SelectedItems"] = string.Join(",", selectedItems);
            TempData["Quantities"] = string.Join(",", quantities.Select(x => $"{x.Key}:{x.Value}"));
            TempData["PaymentMethod"] = paymentMethod;

            return RedirectToAction("CheckoutCart");
        }
        [HttpPost]
        public IActionResult PlaceOrderCart(List<int> selectedItems, Dictionary<int, int> quantities, string phone, string address, string paymentMethod)
        {
            int MaKH = 1;

            var cart = _context.GioHangs
                .Where(x => x.MaKH == MaKH && selectedItems.Contains(x.MaSP))
                .ToList();

            var donHang = new DonHang
            {
                MaKH = MaKH,
                NgayTao = DateTime.Now,
                HinhThucTT = paymentMethod,
                TrangThai = paymentMethod == "ONLINE" ? "Chờ xác nhận" : "Chờ xử lý",
                SoDienThoai = phone,
                DiaChi = address
            };

            _context.DonHangs.Add(donHang);
            _context.SaveChanges();

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

            _context.GioHangs.RemoveRange(cart);
            _context.SaveChanges();

            return Content("<script>alert('Đặt hàng thành công!'); window.location.href='/'</script>", "text/html; charset=utf-8");
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
                donHang.TrangThai = "Chờ xác nhận";
                _context.SaveChanges();
            }

            return Content("<script>alert('Đặt hàng thành công!'); window.location.href='/'</script>", "text/html; charset=utf-8");
        }

    }
}