using System;
using System.Collections.Generic;

namespace QuanLyBanSach.Models;

public partial class ChiTietDonHang
{
    public int MaCtdonHang { get; set; }

    public int MaSach { get; set; }

    public int? SoLuong { get; set; }

    public double? GiamGia { get; set; }

    public decimal? ThanhTien { get; set; }

    public int? MaDonHang { get; set; }

    public virtual DonHang? MaDonHangNavigation { get; set; }

    public virtual Sach MaSachNavigation { get; set; } = null!;
}
