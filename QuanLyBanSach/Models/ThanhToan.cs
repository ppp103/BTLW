using System;
using System.Collections.Generic;

namespace QuanLyBanSach.Models;

public partial class ThanhToan
{
    public int MaTt { get; set; }

    public string? TenTt { get; set; }

    public string? TrangThaiTt { get; set; }

    public int? MaDonHang { get; set; }

    public virtual DonHang? MaDonHangNavigation { get; set; }
}
