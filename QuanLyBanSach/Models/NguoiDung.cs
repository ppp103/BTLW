using System;
using System.Collections.Generic;

namespace QuanLyBanSach.Models;

public partial class NguoiDung
{
    public int MaNd { get; set; }

    public string? HoTenNd { get; set; }

    public string? Email { get; set; }

    public string? MatKhau { get; set; }

    public string? DienThoai { get; set; }

    public string? TaiKhoan { get; set; }

    public string? TrangThaiNd { get; set; }

    public string? Anh { get; set; }

    public DateTime? NgayTao { get; set; }

    public string? DiaChi { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; } = new List<DonHang>();
}
