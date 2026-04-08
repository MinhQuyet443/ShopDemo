using System.ComponentModel.DataAnnotations;

namespace ShopDemo.Models
{
    public class DonHang
    {
        [Key]
        public int MaDH { get; set; }
        public int? MaKH { get; set; }
        public DateTime? NgayTao { get; set; }
        public string? HinhThucTT { get; set; }
        public string? TrangThai { get; set; }
        public string? SoDienThoai { get; set; }
        public string? DiaChi { get; set; }
    }
}