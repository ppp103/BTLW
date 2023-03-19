using System;
using System.Collections.Generic;

namespace QuanLyBanSach.Models;

public partial class ThanhToan
{
    public string MaTt { get; set; } = null!;

    public string? TenTt { get; set; }

    public string? TrangThaiTt { get; set; }

    public string? MaDonHang { get; set; }

    public virtual DonHang? MaDonHangNavigation { get; set; }
}
