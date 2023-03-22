using System;
using System.Collections.Generic;

namespace QuanLyBanSach.Models;

public partial class Sach
{
    public string MaSach { get; set; } = null!;

    public string? TenSach { get; set; }

    public decimal? GiaBan { get; set; }

    public string? MoTa { get; set; }

    public int? SoLuongBs { get; set; }

    public string? Anh { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public int? SoLuongCon { get; set; }

    public string? MaNxb { get; set; }

    public string? MaTg { get; set; }

    public string? MaDm { get; set; }

    public virtual ICollection<BanSaoSach> BanSaoSaches { get; } = new List<BanSaoSach>();

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; } = new List<ChiTietDonHang>();

    public virtual DanhMucSach? MaDmNavigation { get; set; }

    public virtual NhaXuatBan? MaNxbNavigation { get; set; }

    public virtual TacGium? MaTgNavigation { get; set; }
}
