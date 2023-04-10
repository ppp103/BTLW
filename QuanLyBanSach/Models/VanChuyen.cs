using System;
using System.Collections.Generic;

namespace QuanLyBanSach.Models;

public partial class VanChuyen
{
    public int MaVc { get; set; }

    public string? TenVc { get; set; }

    public string? TrangThaiVc { get; set; }

    public int? MaDonHang { get; set; }

    public virtual DonHang? MaDonHangNavigation { get; set; }
}
