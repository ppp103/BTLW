using System;
using System.Collections.Generic;

namespace QuanLyBanSach.Models;

public partial class DanhMucSach
{
    public string MaDm { get; set; } = null!;

	public string? TenDm { get; set; } 

    public virtual ICollection<Sach> Saches { get; } = new List<Sach>();
}
