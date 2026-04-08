using System.ComponentModel.DataAnnotations;

namespace ShopDemo.Models
{
    public class CartViewModel
    {
        [Key]
        public int MaSP { get; set; }
        public string? TenSP { get; set; }
        public string? HinhAnh { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
    }
}
