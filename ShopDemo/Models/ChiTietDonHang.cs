using System.ComponentModel.DataAnnotations;

namespace ShopDemo.Models
{
    public class ChiTietDonHang
    {
        [Key]
        public int MaDH { get; set; }
        public int MaSP { get; set; }
        public int? SoLuong { get; set; }
        public decimal? TongTien { get; set; }
    }
}