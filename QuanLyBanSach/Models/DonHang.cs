using System;
using System.Collections.Generic;

namespace QuanLyBanSach.Models;

public partial class DonHang
{
    public string MaDonHang { get; set; } = null!;

    public string? DiaDiemGh { get; set; }

    public DateTime? NgayDat { get; set; }

    public DateTime? NgayGiao { get; set; }

    public decimal? TongTien { get; set; }

    public string? GhiChu { get; set; }

    public string? TenKh { get; set; }

    public string? TrangThaiDh { get; set; }

    public string? MaNd { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; } = new List<ChiTietDonHang>();

    public virtual NguoiDung? MaNdNavigation { get; set; }

    public virtual ICollection<ThanhToan> ThanhToans { get; } = new List<ThanhToan>();

    public virtual ICollection<VanChuyen> VanChuyens { get; } = new List<VanChuyen>();
}
