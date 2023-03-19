using System;
using System.Collections.Generic;

namespace QuanLyBanSach.Models;

public partial class NhaXuatBan
{
    public string MaNxb { get; set; } = null!;

    public string? TenNxb { get; set; }

    public virtual ICollection<Sach> Saches { get; } = new List<Sach>();
}
