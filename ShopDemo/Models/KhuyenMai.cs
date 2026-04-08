using System.ComponentModel.DataAnnotations;

namespace ShopDemo.Models
{
    public class KhuyenMai
    {
        [Key]
        public int MaKM { get; set; }
        public string? TenKM { get; set; }
        public double PhanTram { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
    }
}
