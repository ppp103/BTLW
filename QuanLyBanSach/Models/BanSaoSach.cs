using System;
using System.Collections.Generic;

namespace QuanLyBanSach.Models;

public partial class BanSaoSach
{
    public int MaBanSao { get; set; }

    public string? TinhTrangBs { get; set; }

    public int MaSach { get; set; }

    public virtual Sach MaSachNavigation { get; set; } = null!;
}
