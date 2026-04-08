using System.ComponentModel.DataAnnotations;

namespace ShopDemo.Models
{
    public class SanPham
    {
        [Key]
        public int MaSP { get; set; }

        public string? TenSP { get; set; }
        public int MaDM { get; set; }
        public string? Mau { get; set; }
        public string? DungLuong { get; set; }
        public decimal GiaTien { get; set; }
        public string? MoTa { get; set; }
        public string? Anh { get; set; }
        public string? HinhAnh { get; set; }
    }
}