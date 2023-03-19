using System;
using System.Collections.Generic;

namespace QuanLyBanSach.Models;

public partial class BanSaoSach
{
    public string MaBanSao { get; set; } = null!;

    public string? TinhTrangBs { get; set; }

    public string MaSach { get; set; } = null!;

    public virtual Sach MaSachNavigation { get; set; } = null!;
}
