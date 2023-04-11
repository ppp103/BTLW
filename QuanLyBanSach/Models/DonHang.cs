using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLyBanSach.Models;

public partial class DonHang
{
    public int MaDonHang { get; set; }

    public string? DiaDiemGh { get; set; }

    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]

    public DateTime? NgayDat { get; set; }
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]

    public DateTime? NgayGiao { get; set; }

    public decimal? TongTien { get; set; }

    public string? GhiChu { get; set; }

    public string? TenKh { get; set; }

    public string? TrangThaiDh { get; set; }

    public int? MaNd { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; } = new List<ChiTietDonHang>();

    public virtual NguoiDung? MaNdNavigation { get; set; }

    public virtual ICollection<ThanhToan> ThanhToans { get; } = new List<ThanhToan>();

    public virtual ICollection<VanChuyen> VanChuyens { get; } = new List<VanChuyen>();
}
