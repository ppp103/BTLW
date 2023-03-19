using System;
using System.Collections.Generic;

namespace QuanLyBanSach.Models;

public partial class ChiTietDonHang
{
    public string MaDonHang { get; set; } = null!;

    public string MaSach { get; set; } = null!;

    public int? SoLuong { get; set; }

    public double? GiamGia { get; set; }

    public decimal? ThanhTien { get; set; }

    public virtual DonHang MaDonHangNavigation { get; set; } = null!;

    public virtual Sach MaSachNavigation { get; set; } = null!;
}
