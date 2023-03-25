using System;
using System.Collections.Generic;

namespace QuanLyBanSach.Models;

public partial class TacGia
{
    public string MaTg { get; set; } = null!;

    public string? TenTg { get; set; }


    public virtual ICollection<Sach> Saches { get; } = new List<Sach>();
	
}
