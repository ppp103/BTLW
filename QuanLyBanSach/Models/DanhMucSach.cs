using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLyBanSach.Models;

public partial class DanhMucSach
{
    public int MaDm { get; set; }

    [Display(Name = "tên danh mục")]
    [Required(ErrorMessage = "Vui lòng nhập {0}")]
    public string? TenDm { get; set; }

    public virtual ICollection<Sach> Saches { get; } = new List<Sach>();
}
