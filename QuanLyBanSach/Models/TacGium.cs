using System;
using System.Collections.Generic;

namespace QuanLyBanSach.Models;

public partial class TacGium
{
    public int MaTg { get; set; }

    public string? TenTg { get; set; }

    public string? Anh { get; set; }

    public virtual ICollection<Sach> Saches { get; } = new List<Sach>();
}
