using System.ComponentModel.DataAnnotations;

namespace ShopDemo.Models
{
    public class KhachHang
    {
        [Key] // 👈 THÊM DÒNG NÀY
        public int MaKH { get; set; }

        public string? TenKH { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}