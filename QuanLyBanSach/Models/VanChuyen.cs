using System;
using System.Collections.Generic;

namespace QuanLyBanSach.Models;

public partial class VanChuyen
{
    public string MaVc { get; set; } = null!;

    public string? TenVc { get; set; }

    public string? TrangThaiVc { get; set; }

    public string? MaDonHang { get; set; }

    public virtual DonHang? MaDonHangNavigation { get; set; }
}
