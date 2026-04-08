using System.ComponentModel.DataAnnotations;

public class NhanVien
{
    [Key]
    public int MaNV { get; set; }
    public string TenNV { get; set; }
    public string DiaChi { get; set; }
    public string QueQuan { get; set; }
    public DateTime NgaySinh { get; set; }
    public string SoDienThoai { get; set; }
    public string Email { get; set; }

    public string Username { get; set; }
    public string Password { get; set; }

    public int MaQuyen { get; set; } // 1 = quản lý, 2 = nhân viên
}