using System.ComponentModel.DataAnnotations;

namespace ShopDemo.Models
{
    public class DanhMuc
    {
        [Key]
        public int MaDM { get; set; }

        public string? TenDM { get; set; }
    }
}