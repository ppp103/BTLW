using System;
using System.Collections.Generic;

namespace QuanLyBanSach.Models;

public partial class Admin
{
    public string MaAd { get; set; } = null!;

    public string? MatKhau { get; set; }

    public string? TenDangNhap { get; set; }

    public string? HoTenAd { get; set; }
}
