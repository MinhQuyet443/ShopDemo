using System.ComponentModel.DataAnnotations;

namespace ShopDemo.Models
{
    public class GioHang
    {
        [Key]
        public int MaGH { get; set; }
        public int MaKH { get; set; }
        public int MaSP { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }


    }
}
